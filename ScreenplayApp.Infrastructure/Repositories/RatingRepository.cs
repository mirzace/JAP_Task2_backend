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
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
