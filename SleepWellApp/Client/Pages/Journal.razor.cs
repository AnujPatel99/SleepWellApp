using SleepWellApp.Shared;
using System.Net.Http.Json;

namespace SleepWellApp.Client.Pages
{
    public partial class Journal
    {
        public int spacing { get; set; } = 2;
        DateTime? date = DateTime.Today;

        public DateTime dateNow = DateTime.Now;
        public string journalContent { get; set; } = "";
        public string errorMessage { get; set; } = ""; // Add this line to store error messages

        IEnumerable<string> MaxCharacters(string value)
        {
            if (value.Length > 500)
            {
                yield return "Text exceeds maximum character limit.";
            }
        }

        public async Task SaveJournal()
        {
            try
            {
                var journalDto = new JournalDto { JournalContent = journalContent };

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync("api/User/SaveJournalEntry", journalDto);

                    if (response.IsSuccessStatusCode)
                    {
                        // Journal entry saved successfully
                        // You can show a success message to the user if needed
                    }
                    else
                    {
                        // Handle error
                        // Display the error message to the user
                        errorMessage = await response.Content.ReadAsStringAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message; // Store the exception message
            }
        }
    }
}

