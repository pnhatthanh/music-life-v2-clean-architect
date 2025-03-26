using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.Auth.DTOs
{
    public class LoginDTO
    {
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
