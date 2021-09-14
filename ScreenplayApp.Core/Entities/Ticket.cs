using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Core.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public Screenplay Screenplay { get; set; }
        public Booking? Booking { get; set; }
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string Location { get; set; }
    }
}
