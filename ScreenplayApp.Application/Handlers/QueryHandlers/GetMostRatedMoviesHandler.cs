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
    public class GetMostRatedMoviesHandler : IRequestHandler<GetMostRatedMoviesQuery, IReadOnlyList<MostRatedMoviesResponse>>
    {
        private readonly IReportRepository _reportRepo;

        public GetMostRatedMoviesHandler(IReportRepository reportRepo)
        {
            _reportRepo = reportRepo;
        }
        public async Task<IReadOnlyList<MostRatedMoviesResponse>> Handle(GetMostRatedMoviesQuery request, CancellationToken cancellationToken)
        {
            var report = await _reportRepo.GetMostRatedMoviesAsync();

            return ScreenplayAppMapper.Mapper.Map<IReadOnlyList<MostRatedMoviesReport>, IReadOnlyList<MostRatedMoviesResponse>>(report);
        }
    }
}
