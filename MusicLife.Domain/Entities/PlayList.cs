using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Domain.Entities
{
    public class PlayList
    {
        [Key]
        public Guid PlayListId { get; set; }=Guid.NewGuid();
        public string PlayListName { get; set; }="";
        public int NumberOfSong { get; set; } = 0;
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual IEnumerable<Song> Songs{ get; set; } =new List<Song>();
    }
}