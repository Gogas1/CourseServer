using CourseServer.Core.Interfaces.Repos;
using CourseServer.Core.Models;
using CourseServer.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Infrastructure.Repos
{
    public class IncomeRepo : IIncomeRepo
    {
        private readonly ProductsDbContext _context;

        public IncomeRepo(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task<Income> AddIncomeAsync(Income income)
        {
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();

            return income;
        }
    }
}
