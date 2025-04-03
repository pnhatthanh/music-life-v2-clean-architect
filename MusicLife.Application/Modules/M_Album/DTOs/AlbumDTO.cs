using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_Album.DTOs
{
    public class AlbumDTO
    {
        public Guid AlbumId { get; set; }
        public string AlbumName { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public string Description { get; set; } = "";
        public int NumberOfSong { get; set; } = 0;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
