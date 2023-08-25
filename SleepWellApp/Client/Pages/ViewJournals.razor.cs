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

        //CODE TO VIEW JOURNALS GOES HERE

        /* protected override async Task OnInitializedAsync()
         {
             var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
             if (UserAuth is not null && UserAuth.IsAuthenticated)
             {
                 ListOfJournals = await GetJournalInfo();
             }
         }

         public async Task<List<JournalDto>> GetJournalInfo()
         {
             return await Http.GetFromJsonAsync<List<JournalDto>>("api/get-journals");
         }
        */

    }
}
