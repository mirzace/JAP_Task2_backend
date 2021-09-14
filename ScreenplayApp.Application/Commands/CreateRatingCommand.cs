using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Commands
{
    public class CreateRatingCommand : IRequest<int>
    {
        public int Rate { get; set; }
        public int ScreenplayId { get; set; }
    }
}
