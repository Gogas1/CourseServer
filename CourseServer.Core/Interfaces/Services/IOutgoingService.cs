using CourseServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Core.Interfaces.Services
{
    public interface IOutgoingService
    {
        Task<Outgoing> Add(Outgoing outgoing);
        Task<IEnumerable<Outgoing>> GetOutgoingsBeetweenDates(DateTime startDate, DateTime endDate);
    }
}
