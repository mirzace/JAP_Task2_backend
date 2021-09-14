﻿using MediatR;
using ScreenplayApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Commands
{
    public class LoginAccountCommand : IRequest<AccountResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
