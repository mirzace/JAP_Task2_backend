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
    public class ReportRepository : IReportRepository
    {
        private readonly DataContext _context;

        public ReportRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<MostRatedMoviesReport>> GetMostRatedMoviesAsync()
        {
            return await _context.MostRatedMoviesReports.FromSqlRaw<MostRatedMoviesReport>("[dbo].[GetTop10RatedMovies]").ToListAsync();
        }

        public async Task<IReadOnlyList<MostSoldMoviesWithoutRatingReport>> GetMostSoldMoviesWithoutRatingAsync()
        {
            return await _context.MostSoldMoviesWithoutRatingReport.FromSqlRaw<MostSoldMoviesWithoutRatingReport>("[dbo].[GetMostSoldMoviesWithoutRating]").ToListAsync();
        }

        public async Task<IReadOnlyList<MostViewedMoviesForAPeriodReport>> GetMostViewedMoviesForAPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.MostViewedMoviesForAPeriodReport.FromSqlRaw<MostViewedMoviesForAPeriodReport>("[dbo].[GetMostViewedMoviesForPeriod] {0}, {1}", startDate, endDate).ToListAsync();
        }
    }
}
