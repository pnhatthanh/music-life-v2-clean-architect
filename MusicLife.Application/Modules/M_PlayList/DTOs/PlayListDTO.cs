using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_PlayList.DTOs
{
    public class PlayListDTO
    {
        public Guid PlayListId { get; set; }
        public string PlayListName { get; set; } = "";
        public int NumberOfSong { get; set; } = 0;
        public Guid UserId { get; set; }
    }
}
