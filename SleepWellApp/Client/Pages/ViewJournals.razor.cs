using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace SleepWellApp.Client.Pages
{
    public partial class ViewJournals
    {

        [Inject]
        public HttpClient Http { get; set; } = new HttpClient();
        [Inject]
        public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }


        //CODE TO VIEW JOURNALS GOES HERE
    }
}
