﻿using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SleepWellApp.Server.Models
{
    public class Journals
    {
        [Key]
        public int Journal_Id { get; set; }
        public int User_Id { get; set; }
        public string? Journal_Content { get; set; }
    }
}