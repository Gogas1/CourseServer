using CourseServer.Core.Interfaces.Services;
using CourseServer.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Core
{
    public static class CoreServicesExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IIncomeService, IncomeSerivce>();
            services.AddScoped<IProductsService, ProductsService>();
        }
    }
}
