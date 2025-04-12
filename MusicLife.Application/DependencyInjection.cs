using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicLife.Application.Mapper;
using MusicLife.Application.Modules.Auth.Services;
using MusicLife.Application.Modules.CurrentUser;
using MusicLife.Application.Modules.M_Album.Services;
using MusicLife.Application.Modules.M_Artist.Services;
using MusicLife.Application.Modules.M_PlayList.Services;
using MusicLife.Application.Modules.M_Song.Services;
using MusicLife.Application.Modules.M_User.Services;
using MusicLife.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(ApplicationMapper));

            // Authentication & Authorization
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = JwtUtil.ValidateToken(config);
            });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                option.AddPolicy("User", policy => policy.RequireRole("User"));
            });

            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IPlayListService, PlayListService>();
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IUserService, UserService>();

            services.AddHttpContextAccessor();
            return services;
        }
    }
}
