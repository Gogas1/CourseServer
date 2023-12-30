﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CourseServer.Infrastructure.Contexts
{
    public static class RegisterDbContextExtensions
    {
        public static void RegisterSqliteDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProductsDbContext>(options => options.UseSqlite(connectionString));
        }
    }
}
