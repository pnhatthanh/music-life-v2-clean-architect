using MusicLife.Application.Modules.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.Auth.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDTO> LoginAsync(LoginDTO req);
        Task<TokenResponseDTO> LoginViaGoogleAsync(string idToken);
        Task LogoutAsync(RefereshTokenDTO token);
        Task<TokenResponseDTO> VerifyAndGenerateTokenAsync(RefereshTokenDTO token);
    }
}
