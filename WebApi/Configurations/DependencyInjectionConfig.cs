using Application;
using Domain.Contracts.Application;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Services.Services;
using Infrastructure.Repositories;

namespace WebApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IRoomActionRepository, RoomActionRepository>();
            services.AddScoped<IRoomActionService, RoomActionService>();
            services.AddScoped<IRoomActionApplication, RoomActionApplication>();

            return services;
        }
    }
}
