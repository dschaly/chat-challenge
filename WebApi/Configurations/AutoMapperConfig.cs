using Arch.EntityFrameworkCore.UnitOfWork.Collections;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Domain.DTOs.Response;
using Domain.Entities;

namespace WebApi.Configurations
{
    public class AutoMapperConfig : Profile
    {
        protected readonly IMapper _mapper;

        public AutoMapperConfig()
        {
            CreateMap<RoomAction, RoomActionResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<Comment, CommentResponse>();
            CreateMap<HighFive, HighFiveResponse>();

            CreateMap<PagedList<RoomAction>, PagedList<RoomActionResponse>>();
        }
    }

    public static class AutoMapperExtension
    {
        /// <summary>
        /// Registrar AutoMapper
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddCollectionMappers();
                mc.AddProfile(new AutoMapperConfig());
            });

            mappingConfig.AssertConfigurationIsValid();
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
