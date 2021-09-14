using AutoMapper;
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
    public class GetScreenplayHandler : IRequestHandler<GetScreenplayQuery, ScreenplayResponse>
    {
        private readonly IScreenplayRepository _screenplayRepo;
        private readonly IMapper _mapper;

        public GetScreenplayHandler(IScreenplayRepository screenplayRepo, IMapper mapper)
        {
            _screenplayRepo = screenplayRepo;
            _mapper = mapper;
        }
        public async Task<ScreenplayResponse> Handle(GetScreenplayQuery request, CancellationToken cancellationToken)
        {
            var screenplayEntity = await _screenplayRepo.GetByIdAsync(request.ScreenplayId);
            var screenplayResponse = ScreenplayAppMapper.Mapper.Map<ScreenplayResponse>(screenplayEntity);
            return screenplayResponse;
        }
    }
}
