using CourseServer.Core.Models;

namespace CourseServer.Core.Interfaces.Services
{
    public interface IIncomeService
    {
        Task<Income> AddIncome(Income income);
        Task<IEnumerable<Income>> GetIncomesBeetweenDates(DateTime startDate, DateTime endDate);
        Task<Income?> GetIncomeById(int id);
    }
}
