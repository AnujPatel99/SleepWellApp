using SleepWellApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using SleepWellApp.Client.Pages;

namespace SleepWellApp.Client.Shared
{
    public partial class JournalCard
    {
        [Parameter]
        [Required]
        public string EntryContent { get; set; } = "";
        [Parameter]
        public EventCallback<string> OnPromptSet { get; set; }
        public DateTime dateNow = DateTime.Now;
        [Inject]
        public HttpClient Http { get; set; } = new HttpClient();
        [Inject]
        public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

        public async Task DeleteJournal()
        {
            try
            {
                var journalDto = new JournalDto { JournalContent = EntryContent };
                var response = await Http.PostAsJsonAsync("api/User/DeleteJournalEntry", journalDto);
                if (response.IsSuccessStatusCode)
                {
                    EntryContent = "Journal entry successfully deleted!";
                }
                else
                {
                    // Handle error
                    // Display the error message to the user
                    EntryContent = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            { // Store the exception message
            }

        }

        private async Task SetPrompt()
        {
            await OnPromptSet.InvokeAsync(EntryContent);
        }
    }
}

