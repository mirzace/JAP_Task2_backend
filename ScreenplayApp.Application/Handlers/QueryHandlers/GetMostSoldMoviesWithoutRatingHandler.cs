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
    public class GetMostSoldMoviesWithoutRatingHandler : IRequestHandler<GetMostSoldMoviesWithoutRatingQuery, IReadOnlyList<MostSoldMoviesWithoutRatingResponse>>
    {
        private readonly IReportRepository _reportRepo;

        public GetMostSoldMoviesWithoutRatingHandler(IReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }
        public async Task<IReadOnlyList<MostSoldMoviesWithoutRatingResponse>> Handle(GetMostSoldMoviesWithoutRatingQuery request, CancellationToken cancellationToken)
        {
            var report = await _reportRepo.GetMostSoldMoviesWithoutRatingAsync();
            
            return ScreenplayAppMapper.Mapper.Map<IReadOnlyList<MostSoldMoviesWithoutRatingReport>, IReadOnlyList<MostSoldMoviesWithoutRatingResponse>>(report);
        }
    }
}
