using MediatR;
using ScreenplayApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Queries
{
    public class GetMostSoldMoviesWithoutRatingQuery : IRequest<IReadOnlyList<MostSoldMoviesWithoutRatingResponse>>
    {
    }
}
