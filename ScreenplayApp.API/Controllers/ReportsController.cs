using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScreenplayApp.Application.Queries;
using ScreenplayApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenplayApp.API.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly IMediator _mediator;
        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("most_rated_movies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IReadOnlyList<MostRatedMoviesResponse>> GetMostRatedMovies([FromQuery] GetMostRatedMoviesQuery command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("most_sold_movies_without_rating")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IReadOnlyList<MostSoldMoviesWithoutRatingResponse>> GetMostSoldMoviesWithoutRating([FromQuery] GetMostSoldMoviesWithoutRatingQuery command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("most_viwed_movies_for_a_period")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IReadOnlyList<MostViewedMoviesForAPeriodResponse>> GetMostViewedMoviesForAPeriod([FromQuery] GetMostViewedMoviesForAPeriodQuery command)
        {
            return await _mediator.Send(command);
        }
    }
}
