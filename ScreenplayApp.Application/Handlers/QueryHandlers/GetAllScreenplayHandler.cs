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

            // Apply filters from search query
            GetFiltersFromSearch(ref request, ref query);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(s => s.Description.Contains(request.Search) || s.Title.Contains(request.Search));
            }
            /*
            // Get exact stars filter
            if (request.ExactStars >= 0 && request.ExactStars <=5)
            {
                query = query.Where(x => (int)x.Ratings.Average(r => r.Rate) == request.ExactStars);
            }
            
            // Get At least stars filter
            if (request.AtLeastStars >= 0 && request.AtLeastStars <= 5)
            {
                query = query.Where(x => x.Ratings.Average(r => r.Rate) >= request.AtLeastStars);
            }

            // Get after year filter
            if (request.NewerThanYear >= 0 && request.NewerThanYear <= DateTime.Today.Year)
            {
                query = query.Where(x => x.ReleaseDate.Year > request.NewerThanYear);
            }

            // Get older than years filter
            if (request.OlderThanYears >= 0)
            {
                query = query.Where(x => x.ReleaseDate.Year <= DateTime.Now.AddYears(request.OlderThanYears * (-1)).Year);
            }
            */

            var screenplays = await query.ToListAsync();
            
            return ScreenplayAppMapper.Mapper.Map<IReadOnlyList<Screenplay>, IReadOnlyList<ScreenplayResponse>>(screenplays);
        }

        private void GetFiltersFromSearch(ref GetAllScreenplayQuery request, ref IQueryable<Screenplay> query)
        {
            bool stringEndsWithPhrase(string s, string phrase) =>
                s.EndsWith(phrase + "s", StringComparison.CurrentCultureIgnoreCase) ||
                s.EndsWith(phrase, StringComparison.CurrentCultureIgnoreCase) ? true : false;

            bool stringStarsWithPhrase(string s, string phrase) =>
                s.StartsWith(phrase, StringComparison.CurrentCultureIgnoreCase);

            // Get all words from search input
            if (!string.IsNullOrWhiteSpace(request.Search))
            { 
                var searchWords = request.Search.Split(' ');

                try
                {
                    // Understand phrase like "5 stars"
                    if (searchWords.Count() == 2 && stringEndsWithPhrase(request.Search, "star"))
                    {
                        if(int.TryParse(searchWords[0], out int numberOfStars))
                        {
                            request.Search = null;
                            request.ExactStars = numberOfStars;

                            if(numberOfStars > 0 && numberOfStars <=5)
                            {
                                query = query.Where(x => (int)x.Ratings.Average(r => r.Rate) == numberOfStars);
                            }
                        }
                    }

                    // Understand phrase like "at least 3 stars"
                    else if (searchWords.Count() == 4 && stringEndsWithPhrase(request.Search, "star")
                        && stringStarsWithPhrase(request.Search, "at least"))
                    {
                        if (int.TryParse(searchWords[2], out int atLeastStars))
                        {
                            request.Search = null;
                            request.AtLeastStars = atLeastStars;

                            if (request.AtLeastStars >= 0 && request.AtLeastStars <= 5)
                            {
                                query = query.Where(x => x.Ratings.Average(r => r.Rate) >= atLeastStars);
                            }
                        }
                    }

                    // Understand phrase like "after 2015"
                    else if (searchWords.Count() == 2 && stringStarsWithPhrase(request.Search, "after"))
                    {
                        if (int.TryParse(searchWords[1], out int year))
                        {
                            request.Search = null;
                            request.NewerThanYear = year;

                            if (request.NewerThanYear >= 0 && request.NewerThanYear <= DateTime.Today.Year)
                            {
                                query = query.Where(x => x.ReleaseDate.Year > year);
                            }
                        }
                    }

                    // Understand phrase like "older than 5 years"
                    else if (searchWords.Count() == 4 && stringEndsWithPhrase(request.Search, "year")
                        && stringStarsWithPhrase(request.Search, "older than"))
                    {
                        if (int.TryParse(searchWords[2], out int olderThanYears))
                        {
                            request.Search = null;
                            request.OlderThanYears = olderThanYears;

                            if (request.OlderThanYears >= 0)
                            {
                                query = query.Where(x => x.ReleaseDate.Year <= DateTime.Now.AddYears(olderThanYears * (-1)).Year);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message);
                }
            }
        }
    }
}
