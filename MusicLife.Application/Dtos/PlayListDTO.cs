using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Dtos
{
    public class PlayListDTO
    {
        public string PlayListName { get; set; } = "";
        public Guid UserId { get; set; }
        public List<Guid> Songs { get; set; } = new List<Guid>();
    }
}
