using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.Auth.DTOs
{
    public class TokenResponseDTO
    {
        public string RefereshToken { get; set; } = "";
        public string AccessToken { get; set; } = "";
    }
}
