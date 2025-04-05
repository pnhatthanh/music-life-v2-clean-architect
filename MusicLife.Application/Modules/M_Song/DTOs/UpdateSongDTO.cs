using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_Song.DTOs
{
    public class UpdateSongDTO
    {
        [Required(ErrorMessage = "Name's song cannot empty")]
        public string SongName { get; set; } = "";
        public IFormFile? ImageFile { get; set; }
        public IFormFile? AudioFile { get; set; }
        public int Duration { get; set; }
        public string? SongLyrics { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public Guid ArtistId { get; set; }
        public int CategoryId { get; set; }
    }
}
