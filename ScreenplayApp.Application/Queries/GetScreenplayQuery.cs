﻿using MediatR;
using ScreenplayApp.Application.Responses;
using ScreenplayApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Queries
{
    public class GetScreenplayQuery : IRequest<ScreenplayResponse>
    {
        public int ScreenplayId { get; set; }
    }
}
