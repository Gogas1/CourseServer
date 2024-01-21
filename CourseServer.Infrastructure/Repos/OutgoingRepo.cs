using CourseServer.Core.Interfaces.Repos;
using CourseServer.Core.Models;
using CourseServer.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseServer.Infrastructure.Repos
{
    public class OutgoingRepo : IOutgoingRepo
    {
        private readonly ProductsDbContext _context;

        public OutgoingRepo(ProductsDbContext context)
        {
            _context = context;
        }

        public async Task<Outgoing> AddAsync(Outgoing newOutgoing)
        {
            _context.Add(newOutgoing);
            await _context.SaveChangesAsync();

            return newOutgoing;
        }
    }
}
