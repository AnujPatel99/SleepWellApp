﻿using Microsoft.AspNetCore.Components;
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
        private bool dense = false;
        private bool hover = true;
        private bool striped = false;
        private bool bordered = false;
        private string searchString1 = "";
        private UserDto selectedItem1 = null;
        private HashSet<UserDto> selectedItems = new HashSet<UserDto>();

        private IEnumerable<UserDto> elements = new List<UserDto>();

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

        private bool FilterFunc1(UserDto element) => FilterFunc(element, searchString1);
        private bool FilterFunc(UserDto element, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (element.UserName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.UserName} {element.FirstName} {element.LastName}".Contains(searchString))
                return true;
            return false;
        }
    }
}
