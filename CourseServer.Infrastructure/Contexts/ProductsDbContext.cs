using CourseServer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseServer.Infrastructure.Contexts
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPricingFeature> ProductFeatures { get; set; }
        public DbSet<ProductIncome> ProductIncomes { get; set; }
        public DbSet<ProductOutgoing> ProductOutgoings { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Outgoing> Outcomes { get; set; }
    }
}
