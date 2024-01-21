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
    public class OutgoingService : IOutgoingService
    {
        private readonly IOutgoingRepo _outgoingRepo;

        public OutgoingService(IOutgoingRepo outgoingRepo)
        {
            _outgoingRepo = outgoingRepo;
        }

        public async Task<Outgoing> Add(Outgoing outgoing)
        {
            foreach (var outgoingProduct in outgoing.OutgoingProducts)
            {
                outgoingProduct.Product.Amount -= outgoingProduct.Amount;
            }

            return await _outgoingRepo.AddAsync(outgoing);
        }

        public async Task<IEnumerable<Outgoing>> GetOutgoingsBeetweenDates(DateTime startDate, DateTime endDate)
        {
            return await _outgoingRepo.GetOutgoingsByConditionAsync(i => i.DateTime >= startDate && i.DateTime <= endDate);
        }
    }
}
