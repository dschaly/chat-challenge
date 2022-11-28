using Application;
using Domain.Contracts.Application;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.DTOs.Request;
using Domain.Services.Services;
using FluentValidation;
using Infrastructure.Repositories;

namespace WebApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Infrastructure
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Services
            services.AddScoped<IRoomActionRepository, RoomActionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoomActionService, RoomActionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoomActionApplication, RoomActionApplication>();


            // Fluent Validation
            services.AddTransient <IValidator<CommentRequest>, CommentRequestValidation>();
            services.AddTransient <IValidator<HighFiveRequest>, HighFiveRequestValidation>();

            return services;
        }
    }
}
