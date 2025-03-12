using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicApi.Infracstructure.Repositories;
using MusicLife.Application.IRepositories;
using MusicLife.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(ApplicationMapper));

            services.AddDbContext<DataContext>
                (option => option.UseSqlServer(configuration["ConnectionStrings:SQLServer"]));
            services.AddScoped<IAlbumRepository,AlbumRepository>();
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IPlayListRepository, PlayListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAlbumSongRepository, AlbumSongRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();  
            services.AddScoped<IUserFavouriteRepository,UserFavouriteRepository>();

            return services;
        }
    }
}
