using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SleepWellApp.Server.Models
{
    public class LikedSounds
    {
        [Key]
        public int Liked_sound_Id { get; set; }
        public int Sound_Id { get; set; }

    }
}