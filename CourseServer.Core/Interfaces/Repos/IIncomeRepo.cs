using CourseServer.Core.Models;
using System.Linq.Expressions;

namespace CourseServer.Core.Interfaces.Repos
{
    public interface IIncomeRepo
    {
        Task<Income> AddIncomeAsync(Income income);
        Task<IEnumerable<Income>> GetIncomesByConditionAsync(Expression<Func<Income, bool>> predicate);
        Task<Income?> GetByIdAsync(int id);
    }
}
