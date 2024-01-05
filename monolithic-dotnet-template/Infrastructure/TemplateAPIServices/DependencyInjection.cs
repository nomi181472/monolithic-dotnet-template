using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateAPIServices
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddTemplateServices(this IServiceCollection services)
        {

            return services;
        }
    }
}
