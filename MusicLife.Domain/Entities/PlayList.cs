using System;
using System.Collections.Generic;
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
        [JsonIgnore]
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? User { get; set; }
        [JsonIgnore]
        public List<Song> Songs{ get; set; } =new List<Song>();
    }
}