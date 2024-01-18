using CourseServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Core.Interfaces.Repos
{
    public interface IIncomeRepo
    {
        Task<Income> AddIncomeAsync(Income income);
        Task<IEnumerable<Income>> GetIncomesByConditionAsync(Expression<Func<Income, bool>> predicate);
    }
}
