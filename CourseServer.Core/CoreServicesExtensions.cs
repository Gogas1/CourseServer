using CourseServer.Core.Interfaces.Services;
using CourseServer.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CourseServer.Core
{
    public static class CoreServicesExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IIncomeService, IncomeSerivce>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IOutgoingService, OutgoingService>();
        }
    }
}
