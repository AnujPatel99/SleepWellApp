namespace SleepWellApp.Client.Pages;

    public partial class JournalEntry
{
    public int spacing { get; set; } = 2;
    DateTime? date = DateTime.Today;

    private IEnumerable<string> MaxCharacters(string ch)
    {
        if (!string.IsNullOrEmpty(ch) && 500 < ch?.Length)
            yield return "Max 500 characters";
    }
}   


