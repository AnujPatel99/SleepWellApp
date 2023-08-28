using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using SleepWellApp.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace SleepWellApp.Client.Pages
{
    public partial class ViewJournals
    {

        [Inject]
        public HttpClient Http { get; set; } = new HttpClient();
        [Inject]
        public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        private List<string> ListOfJournals = new List<string>();
        public UserDto? User = null;
        public int spacing { get; set; } = 2;
        public string? seed { get; set; }
        //public string? userText { get; set; }
        public string? imageURL { get; set; }
        private const string ReplicateAPIToken = "r8_Ci8f12uEXCMuBYzWJGIdBvw8vxbQ3Gf2kkiHI";
        private const string ApiBaseURL = "https://api.replicate.com/v1/";
        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                User = await Http.GetFromJsonAsync<UserDto>("api/User");
                ListOfJournals = await GetJournalEntriesFromApi();
                // ListOfJournals = await GetJournalInfo();
            }
        }
        //CODE TO VIEW JOURNALS GOES HERE
        public async Task<List<string>> GetJournalEntriesFromApi()
        {
            return await Http.GetFromJsonAsync<List<string>>("api/User/GetJournalEntries");
        }

        // Generate Image Code
        private string imageUrl = "images/Logo.png";
        private bool _processing = false;

        private void RefreshImage()
        {
            imageUrl = $"https://picsum.photos/seed/{seed}/1680/1050?{Guid.NewGuid()}";
        }

        async Task ProcessSomething()
        {
            _processing = true;
            await Task.Delay(2000);
            RefreshImage();
            _processing = false;
        }
       
        private async Task GenerateImage()
        {
            /*            _processing = true;
                        if (seed is not null)
                        {
                            var predictionId = await GeneratePredictionAsync(seed);
                            if (!string.IsNullOrEmpty(predictionId))
                            {
                                imageURL = await GetPredictionStatusAndOutputAsync(predictionId);
                            }
                            _processing = false;
                        }*/
            var imageResult = await openAiService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = "Laser cat eyes",
                N = 2,
                Size = StaticValues.ImageStatics.Size.Size256,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "TestUser"
            });


            if (imageResult.Successful)
            {
                Console.WriteLine(string.Join("\n", imageResult.Results.Select(r => r.Url)));
            }

        }
        private async Task<string> GeneratePredictionAsync(string prompt)
        {

            var predictionRequest = new
            {
                version = "92fa143ccefeed01534d5d6648bd47796ef06847a6bc55c0e5c5b6975f2dcdfb",
                input = new
                {
                    prompt = prompt
                }
            };
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", ReplicateAPIToken);
            var predictionJson = JsonSerializer.Serialize(predictionRequest);
            //var apiURL = $"{ApiBaseURL}predictions?Authorization=Token{ReplicateAPIToken}";
            var response = await Http.PostAsync($"{ApiBaseURL}predictions",new StringContent(predictionJson));
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var prediction = JsonSerializer.Deserialize<PredictionResponse>(responseBody);
            if (prediction != null)
            {
                return prediction.id;

            }
            else
            {
                return "Error";
            }
        }
        private async Task<string> GetPredictionStatusAndOutputAsync(string predictionId)
        {
            //var apiURL = $"{ApiBaseURL}predictions/{predictionId}?Authorization=Token{ReplicateAPIToken}";
           
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token",ReplicateAPIToken);
            var response = await Http.GetAsync("${ApiBaseUrl}predictions/{predictionId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var prediction = JsonSerializer.Deserialize<PredictionResponse> (responseBody);

            if (prediction.status == "succeeded")
            {
                return prediction.output;
            }else
            {
                return null;
            }
        }
        private class PredictionResponse
        {
            public string id { get; set; }
            public string status { get; set; }
            public string output { get; set; }
        }


    }
}
