using Microsoft.Extensions.DependencyInjection;
using MusicLife.Application.ExternalServices;
using MusicLife.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLife.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationMapper));

            return services;
        }
    }
}
