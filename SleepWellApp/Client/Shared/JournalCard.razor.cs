using SleepWellApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.ComponentModel.DataAnnotations;

namespace SleepWellApp.Client.Shared
{
    public partial class JournalCard
    {
        [Parameter]
        [Required]
        public string EntryContent { get; set; } = "";
        public DateTime dateNow = DateTime.Now;
    }
}

