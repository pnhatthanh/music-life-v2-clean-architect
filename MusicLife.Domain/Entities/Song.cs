using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicLife.Domain.Entities
{
    public class Song
    {
        [Key]
        public Guid SongId { get; set; }= Guid.NewGuid();
        public string SongName { get; set;}="";
        public string SongPath{ get; set; }="";
        public string SongImagePath{ get; set; }="";
        public int ListenCount {  get; set; }=0;
        public string SongLyrics { get; set; } = "";
        public int Duration{ get; set; }
        public DateTime ReleaseDate{ get; set; }=DateTime.Now;
        public int CategoryId{ get; set; }
        public Guid ArtistId { get; set; }
        public virtual Category? Category{ get; set; }
        public virtual Artist? Artist{ get; set; }
        public virtual IEnumerable<PlayList> PlayLists{ get; set; } =new List<PlayList>();
        public virtual IEnumerable<UserFavourite> UserFavourite { get; set; } = new List<UserFavourite>();
        public virtual IEnumerable<AlbumSong> AlbumSongs { get; set; } = new List<AlbumSong>();

    }
}