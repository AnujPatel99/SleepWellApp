using SleepWellApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace SleepWellApp.Client.Shared;

public partial class AudioCard
{
    [Parameter]
    public int audioID { get; set; } // ID
    [Parameter]
    public string? audioTitle { get; set; } // Title
    [Parameter]
    public string? audioImage { get; set; } // Image
    [Parameter]
    public string? audioDesc { get; set; } // Description
    [Parameter]
    public string? audioLink { get; set; } // Direct link to audio file

    public bool Liked { get; set; }

    private bool isVisible;

    public void OpenOverlay()
    {
        isVisible = true;
        StateHasChanged();
    }

    public void ButtonOnClick(bool liked_toggle)
    {
        Liked = liked_toggle;
        audioDesc = audioID.ToString();

        //await Http.PostAsync($"api/audio/{audioID} / like", null);
    }
}