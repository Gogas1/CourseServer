using CourseServer.Core.Interfaces.Repos;
using CourseServer.Infrastructure.Contexts;
using CourseServer.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static void AddRepos(this IServiceCollection services)
        {
            services.AddScoped<IIncomeRepo, IncomeRepo>();
            services.AddScoped<IProductsRepo, ProductsRepo>();
        }

        public static void RegisterSqliteDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProductsDbContext>(options => options.UseSqlite(connectionString, c => c.MigrationsAssembly("CourseServer.Api")));
        }

        public static void MigrateDb(this IServiceProvider serviceProvider)
        {
            using(var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
