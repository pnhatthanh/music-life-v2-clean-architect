﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicLife.Application.Dtos
{
    public class ArtistDTO
    {

        [Required(ErrorMessage ="Name's artist cannot empty")]
        public string ArtistName { get; set; } = "";
        [Required(ErrorMessage ="Country's artist cannot empty")]
        public string Country { get; set; } = "";
        public int YearOfBirth { get; set; }
        [Required(ErrorMessage ="Image's artist cannot empty")]
        [JsonIgnore]
        public IFormFile? Image { get; set; }
        public int Followers { get; set; } = 0;
        public string About { get; set; } = "";
    }
}
