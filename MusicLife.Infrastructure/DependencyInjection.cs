using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicApi.Infracstructure.Repositories;
using MusicLife.Application.ExternalServices;
using MusicLife.Application.IRepositories;
using MusicLife.Infrastructure.Configurations;
using MusicLife.Infrastructure.ExternalServices;
namespace MusicLife.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //repository
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

            //Configuration
            services.Configure<CloudinarySetting>(configuration.GetSection("CloudinarySetting"));
            services.Configure<RedisSetting>(configuration.GetSection("RedisSetting"));
            //External services
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<ICacheService, CacheService>();


            return services;
        }
    }
}
