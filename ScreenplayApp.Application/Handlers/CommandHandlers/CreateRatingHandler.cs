using MediatR;
using ScreenplayApp.Application.Commands;
using ScreenplayApp.Application.Mapper;
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
    public class CreateRatingHandler : IRequestHandler<CreateRatingCommand, int>
    {
        private readonly IRatingRepository _ratingRepo;
        private readonly IScreenplayRepository _screenplayRepo;

        public CreateRatingHandler(IRatingRepository ratingRepo, IScreenplayRepository screenplayRepo)
        {
            _ratingRepo = ratingRepo;
            _screenplayRepo = screenplayRepo;
        }
        public async Task<int> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            if(request.Rate < 1 || request.Rate > 5)
            {
                throw new ApplicationException("Rate must be between 1 and 5");
            }
            var screenplayEntity = await _screenplayRepo.GetByIdAsync(request.ScreenplayId);
            if (screenplayEntity is null)
            {
                throw new ApplicationException("Screenplay not found");
            }
            var ratingEntitiy = new Rating
            {
                Screenplay = screenplayEntity,
                Rate = request.Rate
            };
            if (ratingEntitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }
            var newRating = await _ratingRepo.AddAsync(ratingEntitiy);
            return newRating.Id;
        }
    }
}
