using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemplateAPIDataAccess;
using TemplateAPIServices.IServices;
using TemplateAPIServices.Services;

namespace TemplateAPIServices
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddTemplateServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();


            services.AddDBContext(configuration);
            return services;
        }
    }
}
