using AutoMapper;
using MusicLife.Application.Exceptions;
using MusicLife.Application.IRepositories;
using MusicLife.Application.Modules.M_User.DTOs;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.M_User.Services { 
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(RegisterDTO registerDTO)
        {
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                throw new BadRequestException("Re-entered password does not match");
            }
            var isExist = await _userRepository.ExistAsync(u => u.UserName == registerDTO.UserName);
            if (isExist == true)
                throw new EmailExistedException();
            User user = new User()
            {
                UserName = registerDTO.UserName!,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                RoleId = 1
            };
            _userRepository.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            User user= await _userRepository.GetByIdAsync(id) ?? throw new UnAuthorizedException();
            return _mapper.Map<UserDTO>(user);
        }
    }
}
