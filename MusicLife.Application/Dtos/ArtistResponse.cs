using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicLife.Application.Dtos
{
    public class ArtistResponse
    {
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } = "";
        public string Country { get; set; } = "";
        public string ImagePath { get; set; } = "";
    }
}
