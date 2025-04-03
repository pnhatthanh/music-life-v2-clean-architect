using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_Artist.DTOs
{
    public class UpdateArtistDTO
    {
        [Required(ErrorMessage = "Name's artist cannot empty")]
        public string ArtistName { get; set; } = "";
        [Required(ErrorMessage = "Country's artist cannot empty")]
        public string Country { get; set; } = "";
        public int YearOfBirth { get; set; }
        public IFormFile? Image { get; set; }
        public string About { get; set; } = "";
    }
}
