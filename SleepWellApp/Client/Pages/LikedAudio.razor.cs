using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace SleepWellApp.Client.Pages
{
    public partial class LikedAudio
    {
        [Inject]
        public HttpClient _httpClient { get; set; } = new HttpClient();
        private List<int> audioIds = new List<int>();

        public LikedAudio()
        { // parameterless constructor because HTML was being mean - Anuj
        }

        protected override async Task OnInitializedAsync()
        {
            audioIds = await GetAudioIdsFromApi();
        }

        public async Task<List<int>> GetAudioIdsFromApi()
        {
            return await _httpClient.GetFromJsonAsync<List<int>>("api/audio/audioIds");
        }
    }
}
