using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }= Guid.NewGuid();
        public string UserName { get; set; }="";
        public string Password { get; set; }="";
        public string? ProviderName{ get; set; }="";
        public string? ProviderId{ get; set; }="";

        public int RoleId { get; set; }
        public virtual Role? Role{ get; set; }

        public virtual IEnumerable<PlayList> PlayLists{ get; set; } = new List<PlayList>();
        public virtual IEnumerable<UserFavourite> UserFavourite{ get; set; } =new List<UserFavourite>();

        public virtual IEnumerable<Token> tokens { get; set; }=new List<Token>();
    }
}