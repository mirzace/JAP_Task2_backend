using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScreenplayApp.Core.Entities;
using ScreenplayApp.Core.Repositories;
using ScreenplayApp.Infrastructure.Data;
using ScreenplayApp.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Infrastructure.Repositories
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<IQueryable<Ticket>> GetAllTicketsQueryAsync()
        {
            return _context.Tickets
                .AsQueryable()
                .AsNoTracking();
        }
    }
}
