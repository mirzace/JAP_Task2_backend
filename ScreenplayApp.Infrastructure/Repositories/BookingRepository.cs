using AutoMapper;
using ScreenplayApp.Core.Entities;
using ScreenplayApp.Core.Repositories;
using ScreenplayApp.Infrastructure.Data;
using ScreenplayApp.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Infrastructure.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
