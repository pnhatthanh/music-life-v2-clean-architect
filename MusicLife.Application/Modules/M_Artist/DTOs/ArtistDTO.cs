using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicLife.Application.Modules.M_Artist.DTOs
{
    public class ArtistDTO
    {
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; } = "";
        public string Country { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public int Followers {  get; set; }
        public string About { get; set; } = "";
    }
}
