using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_Album.DTOs
{
    public class UpdateAlbumDTO
    {
        [Required(ErrorMessage = "Name's album cannot empty")]
        public string AlbumName { get; set; } = "";

        public int ReleaseYear { get; set; } = DateTime.Now.Year;
        [Required(ErrorMessage = "Image's album cannot empty")]
        public IFormFile? ImageFile { get; set; }
        public string Description { get; set; } = "";
    }
}
