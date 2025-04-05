using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLife.Application.Modules.M_Artist.DTOs;

namespace MusicLife.Application.Modules.M_Song.DTOs
{
    public class SongDTO
    {
        public Guid SongId { get; set; }
        public string SongName { get; set; } = "";
        public string SongImagePath { get; set; } = "";
        public string SongPath { get; set; } = "";
        public int ListenCount { get; set; }
        public int Duration { get; set; }
        public ArtistDTO? artist { get; set; }
        public bool IsFavourite { get; set; }
    }
}
