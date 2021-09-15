using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScreenplayApp.Core.Entities
{
    public class MostRatedMoviesReport
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("numberOfRatings")]
        public int number_of_ratings { get; set; }
    }
}
