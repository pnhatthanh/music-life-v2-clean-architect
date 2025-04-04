using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicLife.Domain.Entities
{
    public class Album
    {
        [Key]
        public Guid AlbumId { get; set; }= Guid.NewGuid();
        public string AlbumName { get; set; }="";
        public string ImagePath { get; set; }="";
        public string Description { get; set; } = "";
        public int NumberOfSong { get; set; } = 0;
        public DateTime CreatedTime { get; set; }=DateTime.Now;
        public virtual IEnumerable<AlbumSong> AlbumSongs { get; set; } = new List<AlbumSong>();
    }
}