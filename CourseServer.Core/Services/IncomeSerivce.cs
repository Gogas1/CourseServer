using CourseServer.Core.Interfaces.Repos;
using CourseServer.Core.Interfaces.Services;
using CourseServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Core.Services
{
    public class IncomeSerivce : IIncomeService
    {
        private readonly IIncomeRepo _incomeRepo;

        public IncomeSerivce(IIncomeRepo incomeRepo)
        {
            _incomeRepo = incomeRepo;
        }

        public async Task<Income> AddIncome(Income income)
        {
            foreach (var incomingProduct in income.IncomeProducts)
            {
                incomingProduct.Product.Amount += incomingProduct.Amount;
            }

            return await _incomeRepo.AddIncomeAsync(income);
        }
    }
}
