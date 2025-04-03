using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_PlayList.DTOs
{
    public class CreatePlayListDTO
    {
        public string PlayListName { get; set; } = "";
        public List<Guid> Songs { get; set; } = new List<Guid>();
    }
}
