using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SleepWellApp.Server.Models
{
    public class SoundBoards
    {
        [Key]
        public int Sound_Id { get; set; }
        public string? Genre { get; set; }
        public string? Artist { get; set; }
        public string? Title { get; set; }
    }
}