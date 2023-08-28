using SleepWellApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using SleepWellApp.Client.HttpRepository;
using SleepWellApp.Client.Shared;

namespace SleepWellApp.Client.Pages;

public partial class Index
{
    [Inject]
    public HttpClient Http { get; set; } = new HttpClient();
    [Inject]
    public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
    public UserDto? User = null;
    public IUserInfoHttpRepository UserInfoHttpRepository { get; set; }

    private List<int> likedAudioIds = new List<int>();

    protected override async Task OnInitializedAsync()
    {
        var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
        if (UserAuth is not null && UserAuth.IsAuthenticated)
        {
            User = await Http.GetFromJsonAsync<UserDto>("api/User");
            likedAudioIds = await GetAudioIdsFromApi();

        }
    }

    private string searchText = "";

    public async Task<List<int>> GetAudioIdsFromApi()
    {
        return await Http.GetFromJsonAsync<List<int>>("api/audio/audioIds");
    }

    private List<AudioCard> filteredAudioCards =>
        string.IsNullOrWhiteSpace(searchText)
            ? new List<AudioCard>()
            : audioCards.Where(card => card.audioTitle.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
    

}
