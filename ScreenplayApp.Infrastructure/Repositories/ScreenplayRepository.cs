using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ScreenplayApp.Application.Queries;
using ScreenplayApp.Application.Responses;
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
    public class ScreenplayRepository : Repository<Screenplay>, IScreenplayRepository
    {
        private readonly IMapper _mapper;

        public ScreenplayRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public new async Task<Screenplay> GetByIdAsync(int id)
        {
            return await _context.Screenplays
                .Include(x => x.Ratings)
                //.Include(x => x.Actors)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IQueryable<Screenplay>> GetAllScreenplaysQueryAsync()
        {
            return _context.Screenplays
                .AsQueryable()
                .AsNoTracking();
        }
    }
}
