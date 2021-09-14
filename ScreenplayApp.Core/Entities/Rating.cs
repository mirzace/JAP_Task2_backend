using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Core.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public Screenplay Screenplay { get; set; }
    }
}
