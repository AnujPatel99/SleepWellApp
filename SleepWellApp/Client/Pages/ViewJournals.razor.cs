using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SleepWellApp.Shared;
using System.Net.Http.Json;


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
            imageUrl = $"https://picsum.photos/1680/1050?{Guid.NewGuid()}";
        }

        async Task ProcessSomething()
        {
            _processing = true;
            await Task.Delay(2000);
            RefreshImage();
            _processing = false;
        }
    }
}
