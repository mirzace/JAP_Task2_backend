using MediatR;
using ScreenplayApp.Application.Mapper;
using ScreenplayApp.Application.Queries;
using ScreenplayApp.Application.Responses;
using ScreenplayApp.Core.Entities;
using ScreenplayApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Handlers.QueryHandlers
{
    public class GetMostViewedMoviesForAPeriodHandler : IRequestHandler<GetMostViewedMoviesForAPeriodQuery, IReadOnlyList<MostViewedMoviesForAPeriodResponse>>
    {
        private readonly IReportRepository _reportRepo;

        public GetMostViewedMoviesForAPeriodHandler(IReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }

        public async Task<IReadOnlyList<MostViewedMoviesForAPeriodResponse>> Handle(GetMostViewedMoviesForAPeriodQuery request, CancellationToken cancellationToken)
        {
            var report = await _reportRepo.GetMostViewedMoviesForAPeriodAsync(request.StartDate, request.EndDate);

            return ScreenplayAppMapper.Mapper.Map<IReadOnlyList<MostViewedMoviesForAPeriodReport>, IReadOnlyList<MostViewedMoviesForAPeriodResponse>>(report);
        }
    }
}
