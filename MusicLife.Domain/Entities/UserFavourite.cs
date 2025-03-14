using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Domain.Entities
{
    public class UserFavourite
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public Guid SongId { get; set; }
        public virtual Song? Song { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
