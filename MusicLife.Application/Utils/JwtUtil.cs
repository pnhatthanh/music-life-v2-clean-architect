using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusicLife.Application.Common;
using MusicLife.Application.Modules.Auth.DTOs;
using MusicLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Utils
{
    public class JwtUtil
    {
        private static JwtSecurityTokenHandler _jwtHandler = new JwtSecurityTokenHandler();
        private static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
        private static string GenerateToken(IEnumerable<Claim> claims, DateTime expires, JwtParams _jwtSetting)
        {
            var securityKey = GetSymmetricSecurityKey(_jwtSetting.SecurityKey);
            var tokenOption = new JwtSecurityToken(
                     issuer: _jwtSetting.Issuer,
                     audience: _jwtSetting.Audience,
                     claims: claims,
                     expires: expires,
                     signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));
            return _jwtHandler.WriteToken(tokenOption);
        }
        public static TokenResponseDTO GenerateAccessAndRefereshToken(User user, IConfiguration config)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Role!.RoleName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            };
            var jwtSetting = new JwtParams(config);
            var accessTokenExpired = DateTime.UtcNow.AddMinutes(jwtSetting.AccessTokenExpiration);
            var refershTokenExpired = DateTime.UtcNow.AddMinutes(jwtSetting.RefereshTokenExpiration);
            return new TokenResponseDTO
            {
                AccessToken = GenerateToken(claims, accessTokenExpired, jwtSetting),
                RefereshToken = GenerateToken(claims, refershTokenExpired, jwtSetting),
            };
        }
        public static TokenValidationParameters ValidateToken(IConfiguration jwtSetting)
        {
            var _jwtSetting=new JwtParams(jwtSetting);
            return new TokenValidationParameters
            {
                IssuerSigningKey = GetSymmetricSecurityKey(_jwtSetting.SecurityKey),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSetting.Issuer,
                ValidAudience = _jwtSetting.Audience
            };
        }

    }
}
