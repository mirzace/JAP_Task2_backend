using MediatR;
using ScreenplayApp.Application.Responses;
using ScreenplayApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Queries
{
    public class GetAllScreenplayQuery : IRequest<IReadOnlyList<ScreenplayResponse>>
    {
        public string Category { get; set; } = "Movie";
        public string Search { get; set; }
        public int OlderThanYears { get; set; }
        public int NewerThanYear { get; set; }
        public int AtLeastStars { get; set; }
        public int ExactStars { get; set; }
    }
}
