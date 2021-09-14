using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Core.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public AppUser AppUser { get; set; }
    }
}
