using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Domain.Entities
{
    public class AlbumSong
    {
        public Guid AlbumId { get; set; }
        public Album? Album { get; set; }

        public Guid SongId { get; set; }
        public Song? Song { get; set; }
    }
}
