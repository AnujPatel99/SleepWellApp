﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SleepWellApp.Shared;
using System.Net.Http.Json;

namespace SleepWellApp.Client.Pages
{
    public partial class LikedAudio
    {
        [Inject]
        public HttpClient _httpClient { get; set; } = new HttpClient();
        [Inject]
        public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        private List<int> audioIds = new List<int>();
        public UserDto? User = null;

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                audioIds = await GetAudioIdsFromApi();
                User = await Http.GetFromJsonAsync<UserDto>("api/User");

            }
        }

        public async Task<List<int>> GetAudioIdsFromApi()
        {
            return await _httpClient.GetFromJsonAsync<List<int>>("api/audio/audioIds");
        }
    }
}
