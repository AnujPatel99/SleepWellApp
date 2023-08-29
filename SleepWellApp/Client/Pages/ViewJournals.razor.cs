using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using OpenAI;
using OpenAI.Managers;
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
        public string? imageURL { get; set; }
        public string? interpVal { get; set; }

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

        public async Task<List<string>> GetJournalEntriesFromApi()
        {
            return await Http.GetFromJsonAsync<List<string>>("api/User/GetJournalEntries");
        }

        // Generate Image Code
        private string imageUrl = "images/Logo.png";
        private bool _processing = false;

        [Inject]
        public IConfiguration? Configuration { get; set; }

        private async Task GenerateImageAndInterpretation()
        {
            _processing = true;
            var apiKey = Configuration["OpenAIServiceOptions:ApiKey"];
            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = "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"
            });
            var imageResult = await openAiService.Image.CreateImage(new ImageCreateRequest
            {
                Prompt = seed,
                N = 1,
                Size = StaticValues.ImageStatics.Size.Size256,
                ResponseFormat = StaticValues.ImageStatics.ResponseFormat.Url,
                User = "TestUser"
            });

            var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("Given any input from the User, interpret the input as if it were a dream."),
                    ChatMessage.FromUser(seed)
                },
                Model = Models.ChatGpt3_5Turbo,
            });
            if (completionResult.Successful)
            {
                Console.WriteLine(completionResult.Choices.First().Message.Content);
                interpVal = completionResult.Choices.First().Message.Content;
            }

            if (imageResult.Successful)
            {
                Console.WriteLine(string.Join("\n", imageResult.Results.Select(r => r.Url)));
                imageUrl = imageResult.Results[0].Url;
            }
            _processing = false;

        }

    }
}