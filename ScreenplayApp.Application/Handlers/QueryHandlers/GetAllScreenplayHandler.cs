using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetAllScreenplayHandler : IRequestHandler<GetAllScreenplayQuery, IReadOnlyList<ScreenplayResponse>>
    {
        private readonly IScreenplayRepository _screenplayRepo;

        public GetAllScreenplayHandler(IScreenplayRepository screenplayRepo)
        {
            _screenplayRepo = screenplayRepo;
        }
        public async Task<IReadOnlyList<ScreenplayResponse>> Handle(GetAllScreenplayQuery request, CancellationToken cancellationToken)
        {

            var query = await _screenplayRepo.GetAllScreenplaysQueryAsync();

            query = query.Where(x => x.Category == request.Category)
                .OrderByDescending(x => x.Ratings.Average(a => a.Rate));

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(s => s.Description.Contains(request.Search) || s.Title.Contains(request.Search));
            }

            var screenplays = await query.ToListAsync();
            
            return ScreenplayAppMapper.Mapper.Map<IReadOnlyList<Screenplay>, IReadOnlyList<ScreenplayResponse>>(screenplays);
        }
    }
}
