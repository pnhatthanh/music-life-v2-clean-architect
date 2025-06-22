using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MusicApi.Infracstructure.Repositories;
using MusicLife.Application.ExternalServices;
using MusicLife.Application.IRepositories;
using MusicLife.Infrastructure.Configurations;
using MusicLife.Infrastructure.ExternalServices;
using MusicLife.Infrastructure.Repositories;
using StackExchange.Redis;
namespace MusicLife.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //repository
            services.AddDbContext<DataContext>
                (option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAlbumRepository,AlbumRepository>();
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IPlayListRepository, PlayListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAlbumSongRepository, AlbumSongRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();  
            services.AddScoped<IUserFavouriteRepository,UserFavouriteRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            //Configuration
            services.Configure<CloudinarySetting>(configuration.GetSection("CloudinarySetting"));
            services.Configure<RedisSetting>(configuration.GetSection("RedisSetting"));

            //External services
            var redisSetting = configuration.GetSection("RedisSetting").Get<RedisSetting>();
            if (redisSetting !=null && redisSetting.Enable == true)
            {
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisSetting.ConnectionString!));
                services.AddStackExchangeRedisCache(option => option.Configuration = redisSetting.ConnectionString);
                services.AddScoped<ICacheService, CacheService>();
            }      
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            return services;
        }
    }
}
