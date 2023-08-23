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

    protected override async Task OnInitializedAsync()
    {
        var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
        if (UserAuth is not null && UserAuth.IsAuthenticated)
        {
            User = await Http.GetFromJsonAsync<UserDto>("api/User");
        }
    }

    private string searchText = "";

    private List<AudioCard> filteredAudioCards =>
        string.IsNullOrWhiteSpace(searchText)
            ? audioCards
            : audioCards.Where(card => card.audioTitle.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
    

}
