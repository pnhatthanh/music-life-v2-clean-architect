using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_User.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string ProviderName { get; set; } = "";
        public string ProviderId { get; set; } = "";
    }
}
