using CourseServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Core.Interfaces.Services
{
    public interface IIncomeService
    {
        Task<Income> AddIncome(Income income);
        Task<IEnumerable<Income>> GetIncomesBeetweenDates(DateTime startDate, DateTime endDate);
        Task<Income?> GetIncomeById(int id);
    }
}
