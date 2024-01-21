using CourseServer.Core.Models;
using System.Linq.Expressions;

namespace CourseServer.Core.Interfaces.Repos
{
    public interface IOutgoingRepo
    {
        Task<Outgoing> AddAsync(Outgoing newOutgoing);
        Task<IEnumerable<Outgoing>> GetOutgoingsByConditionAsync(Expression<Func<Outgoing, bool>> predicate);
    }
}
