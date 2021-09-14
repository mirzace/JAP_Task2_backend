using MediatR;
using ScreenplayApp.Application.Commands;
using ScreenplayApp.Application.Mapper;
using ScreenplayApp.Application.Responses;
using ScreenplayApp.Core.Entities;
using ScreenplayApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Handlers.CommandHandlers
{
    public class CreateScreenplayHandler : IRequestHandler<CreateScreenplayCommand, ScreenplayResponse>
    {
        private readonly IScreenplayRepository _screenplayRepo;

        public CreateScreenplayHandler(IScreenplayRepository screenplayRepo)
        {
            _screenplayRepo = screenplayRepo;
        }
        public async Task<ScreenplayResponse> Handle(CreateScreenplayCommand request, CancellationToken cancellationToken)
        {
            var screenplayEntitiy = ScreenplayAppMapper.Mapper.Map<Screenplay>(request);
            if (screenplayEntitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var newScreenplay = await _screenplayRepo.AddAsync(screenplayEntitiy);
            var screenplayResponse = ScreenplayAppMapper.Mapper.Map<ScreenplayResponse>(newScreenplay);
            return screenplayResponse;
        }
    }
}
