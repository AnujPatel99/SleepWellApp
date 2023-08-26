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
        private List<JournalDto> ListOfJournals = new List<JournalDto>();
        public UserDto? User = null;

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                User = await Http.GetFromJsonAsync<UserDto>("api/User");
               // ListOfJournals = await GetJournalInfo();
            }
        }
        //CODE TO VIEW JOURNALS GOES HERE

        
        /*
         public async Task<List<JournalDto>> GetJournalInfo()
         {
             return await Http.GetFromJsonAsync<List<JournalDto>>("api/get-journals");
         }
        */

    }
}
