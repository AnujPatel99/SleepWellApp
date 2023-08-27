using SleepWellApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.ComponentModel.DataAnnotations;

namespace SleepWellApp.Client.Shared;

public partial class AudioCard
{
    [Parameter] [Required]
    public int audioID { get; set; } // ID
    [Parameter] [Required]
    public string? audioTitle { get; set; } // Title
    [Parameter]
    public string? audioImage { get; set; } // Image
    [Parameter]
    public string? audioDesc { get; set; } // Description
    [Parameter] [Required]
    public string? audioLink { get; set; } // Direct link to audio file
    [Parameter]
    public bool Liked { get; set; }

    private bool isVisible;

    public int currentPlayingAudio = 0;

    public int timesTapped = 0;

    public int durationToPlayFor;

    public void OpenOverlay()
    {
        isVisible = true;
        currentPlayingAudio = audioID;
        /*foreach (var card in otherCards)
        {
            if (card != null && card.audioID != card.currentPlayingAudio)
            {
                card.currentPlayingAudio = -1;
            }
        }*/
        StateHasChanged();
    }

    public void OnDurationChanged(string s)
    {
        durationToPlayFor = Int32.Parse(s);
    }

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

    public async Task ButtonOnClick(bool liked_toggle)
    {
        Liked = liked_toggle;
        var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
        try
        {
            if (User is null)
            {
                audioDesc = "You are not logged in! Login to like a sound.";
            }
            else if (Liked)
            {
                audioDesc = "Sound succesfully liked, please go to the Liked Audio page to view it!";
            }
            else
            {
                audioDesc = "Sound unliked. You clicked unlike on accident right?";
            }
            await Http.PostAsync($"api/audio/{audioID}/like/{Liked}", null);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public void ShowGrandma()
    {
        timesTapped++;
        StateHasChanged();
    }
}