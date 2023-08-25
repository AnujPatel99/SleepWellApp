using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SleepWellApp.Shared;
using System.Net.Http.Json;

namespace SleepWellApp.Client.Pages
{
    public partial class Journal
    {
        [Inject]
        public HttpClient Http { get; set; } = new HttpClient();
        [Inject]
        public AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        public int spacing { get; set; } = 2;
        public DateTime dateNow = DateTime.Now;
        public string journalContent { get; set; } = "";
        public string Message { get; set; } = ""; // Add this line to store error messages

        IEnumerable<string> MaxCharacters(string value)
        {
            if (value.Length > 500)
            {
                yield return "Text exceeds maximum character limit.";
            }
        }

        public async Task SaveJournal()
        {
            if (string.IsNullOrWhiteSpace(journalContent)) {
                Message = "I'm sure you've dreamt about something! Don't leave us hanging!";
            }
            else
            {
                try
                {
                    var journalDto = new JournalDto { JournalContent = journalContent };
                    var response = await Http.PostAsJsonAsync("api/User/SaveJournalEntry", journalDto);

                    if (response.IsSuccessStatusCode)
                    {
                        Message = "Journal entry successfully logged!";
                    }
                    else
                    {
                        // Handle error
                        // Display the error message to the user
                        Message = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message; // Store the exception message
                }
            }
        }
    }
}

