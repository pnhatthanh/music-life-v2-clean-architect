using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLife.Application.Modules.Auth.DTOs;
using MusicLife.Application.Modules.Auth.Services;

namespace MusicLife.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO req)
        {
            var token = await _authService.LoginAsync(req);
            return Ok(token);
        }
        [HttpPost("login/via-google")]
        public Task<IActionResult> LoginViaGoogle([FromBody] RefereshTokenDTO tokenRequest)
        {
            throw new NotImplementedException();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefereshTokenDTO tokenDTO)
        {
            await _authService.LogoutAsync(tokenDTO);
            return Ok();
        }
        [HttpPost("referesh")]
        public async Task<IActionResult> RefereshAccout([FromBody] RefereshTokenDTO _token)
        {
            var token = await _authService.VerifyAndGenerateTokenAsync(_token);
            return Ok(token);
        }
    }
}
