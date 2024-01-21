using CourseServer.Core.Models;

namespace CourseServer.Core.Interfaces.Services
{
    public interface IOutgoingService
    {
        Task<Outgoing> Add(Outgoing outgoing);
        Task<IEnumerable<Outgoing>> GetOutgoingsBeetweenDates(DateTime startDate, DateTime endDate);
    }
}
