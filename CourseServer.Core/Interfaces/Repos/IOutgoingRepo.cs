using CourseServer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Core.Interfaces.Repos
{
    public interface IOutgoingRepo
    {
        Task<Outgoing> AddAsync(Outgoing newOutgoing);
        Task<IEnumerable<Outgoing>> GetOutgoingsByConditionAsync(Expression<Func<Outgoing, bool>> predicate);
    }
}
