using ScreenplayApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenplayApp.API.Extensions
{
    public static class RateExtensions
    {
        public static double CalculateRate(this IEnumerable<Rating> ratings)
        {
            if (ratings.Count() == 0) return 0;
            return ratings.Average(r => r.Rate);
        }
    }
}
