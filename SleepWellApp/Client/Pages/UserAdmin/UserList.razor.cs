using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SleepWellApp.Shared;
using System.Net.Http.Json;

namespace SleepWellApp.Client.Pages.UserAdmin
{
    public partial class UserList
    {
        [Inject]
        public HttpClient Http { get; set; } = new HttpClient();
        [Inject]
        public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        private List<UserDto> ListOfUsers = new List<UserDto>();

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                ListOfUsers = await GetUserInfo();
            }
        }

        public async Task<List<UserDto>> GetUserInfo()
        {
            return await Http.GetFromJsonAsync<List<UserDto>>("api/get-users");
        }
    }
}
