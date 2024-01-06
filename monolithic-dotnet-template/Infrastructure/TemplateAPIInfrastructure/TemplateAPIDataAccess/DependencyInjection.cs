using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TemplateAPIDataAccess.DBContext;

namespace TemplateAPIDataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDBContext(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            //serviceDescriptors.AddDbContext<TemplateDBContext>(options => options.UseInMemoryDatabase("database"));


            var connectionString = configuration.GetSection("ConnectionStrings:db").Value;
            serviceDescriptors.AddDbContext<TemplateDBContext>(

                options => options.UseNpgsql(connectionString)
                );
            serviceDescriptors.AddScoped<IUnitOfWork, UnitOfWork>();

            return serviceDescriptors;

            


        }
    }
}
