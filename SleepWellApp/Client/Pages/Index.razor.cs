using SleepWellApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace SleepWellApp.Client.Pages;

public partial class Index
{
    [Inject]
    public HttpClient Http { get; set; } = new HttpClient();
    [Inject]
    public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
    public UserDto? User = null;
    protected override async Task OnInitializedAsync()
    {
        var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
        if (UserAuth is not null && UserAuth.IsAuthenticated)
        {
            User = await Http.GetFromJsonAsync<UserDto>("api/User");
        }
    }
}
