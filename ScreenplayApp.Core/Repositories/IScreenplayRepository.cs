using ScreenplayApp.Core.Entities;
using ScreenplayApp.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Core.Repositories
{
    public interface IScreenplayRepository : IRepository<Screenplay>
    {
        Task<IQueryable<Screenplay>> GetAllScreenplaysQueryAsync();
    }
}
