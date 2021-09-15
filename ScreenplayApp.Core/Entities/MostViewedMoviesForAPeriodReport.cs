using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScreenplayApp.Core.Entities
{
    public class MostViewedMoviesForAPeriodReport
    {
        public int Id { get; set; }
        public int Title { get; set; }
        [JsonPropertyName("screeningNumber")]
        public int screening_number { get; set; }
    }
}
