﻿using CourseServer.Core.Interfaces.Repos;
using CourseServer.Core.Interfaces.Services;
using CourseServer.Core.Models;

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

        public async Task<Income?> GetIncomeById(int id)
        {
            return await _incomeRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Income>> GetIncomesBeetweenDates(DateTime startDate, DateTime endDate)
        {
            return await _incomeRepo.GetIncomesByConditionAsync(i => i.CreatedAt >= startDate && i.CreatedAt <= endDate);
        }
    }
}
