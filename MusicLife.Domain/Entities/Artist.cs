using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Domain.Entities
{
    public class Artist
    {
        [Key]
        public Guid ArtistId { get; set; }=Guid.NewGuid();
        public string ArtistName { get; set; }="";
        public string Country{ get; set; }="";
        public int YearOfBirth{ get; set; }
        public string ImagePath{ get; set; }="";
        public int Followers { get; set; } = 0;
        public string About { get; set; } = "";
        [JsonIgnore]
        public virtual List<Song>? Songs { get; set; }  
    }
}