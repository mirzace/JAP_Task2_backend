using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Responses
{
    public class MostSoldMoviesWithoutRatingResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        [JsonPropertyName("ticketsSold")]
        public int tickets_sold { get; set; }
    }
}
