using MusicLife.Application.Modules.M_User.DTOs;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_User.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(RegisterDTO registerDTO);
        Task<UserDTO> GetUserByIdAsync(Guid id);
       
    }
}
