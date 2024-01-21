using CourseServer.Core.Interfaces.Repos;
using CourseServer.Core.Models;
using CourseServer.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<Income?> GetByIdAsync(int id)
        {
            return await _context.Incomes
                .Include(_ => _.IncomeProducts)
                .ThenInclude(_ => _.Product)
                .ThenInclude(_ => _.TypeFeature)
                .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<IEnumerable<Income>> GetIncomesByConditionAsync(Expression<Func<Income, bool>> predicate)
        {
            return await _context.Incomes
                .Where(predicate)
                .ToListAsync();
        }
    }
}
