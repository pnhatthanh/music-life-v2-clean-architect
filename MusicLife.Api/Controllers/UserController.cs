using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLife.Application.Modules.M_User.DTOs;
using MusicLife.Application.Modules.M_User.Services;

namespace MusicLife.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            await _userService.CreateUserAsync(registerDTO);
            return Ok();
        }
    }
}
