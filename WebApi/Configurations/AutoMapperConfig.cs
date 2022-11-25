using AutoMapper;
using AutoMapper.EquivalencyExpression;

namespace WebApi.Configurations
{
    public class AutoMapperConfig : Profile
    {
        protected readonly IMapper _mapper;

        public AutoMapperConfig()
        {
            //CreateMap<VehicleProject, VehicleProjectResponse>()
            //    .ReverseMap();
            //CreateMap<VehicleProject, VehicleProjectPost>()
            //    .ReverseMap();
            //CreateMap<VehicleProject, VehicleProjectPut>()
            //    .ReverseMap();
            //CreateMap<PagedList<VehicleProject>, PagedList<VehicleProjectResponse>>()
            //    .ReverseMap();
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
