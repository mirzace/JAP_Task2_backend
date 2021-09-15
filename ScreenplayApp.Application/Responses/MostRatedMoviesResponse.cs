using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Responses
{
    public class MostRatedMoviesResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("numberOfRatings")]
        public int number_of_ratings { get; set; }
    }
}
