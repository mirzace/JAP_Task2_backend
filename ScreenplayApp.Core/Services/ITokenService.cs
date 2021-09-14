using ScreenplayApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Core.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
