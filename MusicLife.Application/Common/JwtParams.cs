﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application.Common
{
    public class JwtParams
    {
        public string Issuer {  get; set; }
        public string Audience {  get; set; }
        public string SecurityKey {  get; set; }
        public double RefereshTokenExpiration {  get; set; }
        public double AccessTokenExpiration { get; set; }
        public string Subject {  get; set; }
        public JwtParams(IConfiguration jwtSetting)
        {
            Issuer = jwtSetting["Authentication:Jwt:Issuer"] ?? throw new InvalidOperationException();
            Audience = jwtSetting["Authentication:Jwt:Audience"] ?? throw new InvalidOperationException();
            SecurityKey = jwtSetting["Authentication:Jwt:Key"]?? throw new InvalidOperationException();
            RefereshTokenExpiration = Double.Parse(jwtSetting["Authentication:Jwt:RefereshTokenExpirationMinuteTime"]
                ?? throw new InvalidOperationException());
            AccessTokenExpiration = Double.Parse(jwtSetting["Authentication:Jwt:AccessTokenExpirationMinuteTime"]
                ?? throw new InvalidOperationException());
            Subject = jwtSetting["Authentication:Jwt:Subject"]?? throw new InvalidOperationException();
        }
    }
}
