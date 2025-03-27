using Microsoft.Extensions.Configuration;
using MusicLife.Application.Exceptions;
using MusicLife.Application.IRepositories;
using MusicLife.Application.Modules.Auth.DTOs;
using MusicLife.Application.Utils;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Modules.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        public AuthService(ITokenRepository tokenRepository, IUserRepository userRepository
            ,IConfiguration config, IUnitOfWork unitOfWork) { 
            _tokenRepository= tokenRepository;
            _userRepository= userRepository;
            _config= config;
            _unitOfWork= unitOfWork;
        }
        public async Task<TokenResponseDTO> LoginAsync(LoginDTO req)
        {
            var user = await _userRepository.FirstOrDefaultAsync(
                    expressions: u => u.UserName == req.Email,
                    includes:  u=>u.Role!
                )
                ?? throw new NotFoundException("Incorrect username or password");
            if (!BCrypt.Net.BCrypt.Verify(req.Password, user.Password))
            {
                throw new BadRequestException("Incorrect username or password");
            }
            var tokenResponse = JwtUtil.GenerateAccessAndRefereshToken(user, _config);
            var refereshToken = new Token
            {
                RefereshToken= tokenResponse.RefereshToken,
                CreatedAt=DateTimeOffset.Now.ToUnixTimeSeconds(),
                IsRevoked=false,
                userId=user.UserId
            };
            _tokenRepository.Add(refereshToken);
            await _unitOfWork.SaveChangesAsync();
            return tokenResponse;
        }
        public Task<TokenResponseDTO> LoginViaGoogleAsync(string idToken)
        {
            throw new NotImplementedException();
        }
        public async Task LogoutAsync(RefereshTokenDTO token)
        {
            var refereshToken = await _tokenRepository.FirstOrDefaultAsync(t=>t.RefereshToken==token.RefereshToken) 
                ?? throw new NotFoundException("Invalid token");
            _tokenRepository.Delete(refereshToken);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<TokenResponseDTO> VerifyAndGenerateTokenAsync(RefereshTokenDTO token)
        {
            var _token = await _tokenRepository.FirstOrDefaultAsync(t => t.RefereshToken == token.RefereshToken)
                        ?? throw new NotFoundException("Invalid token");
            if (_token.IsRevoked == true || _token.ExpirationTime < DateTimeOffset.Now.ToUnixTimeSeconds())
            {
                throw new BadRequestException("Token is expired");
            }
            _tokenRepository.Delete(_token);
            var user = await _userRepository.FirstOrDefaultAsync(
                    expressions: u=>u.UserId==_token.userId,
                    includes: u=>u.Role!
                    )
                       ?? throw new Exception("User not found");
            var tokenResponse = JwtUtil.GenerateAccessAndRefereshToken(user, _config);
            var refereshToken = new Token
            {
                RefereshToken = tokenResponse.RefereshToken,
                CreatedAt = DateTimeOffset.Now.ToUnixTimeSeconds(),
                IsRevoked = false,
                userId = user.UserId
            };
            _tokenRepository.Add(refereshToken);
            await _unitOfWork.SaveChangesAsync();
            return tokenResponse;
        }
    }
}
