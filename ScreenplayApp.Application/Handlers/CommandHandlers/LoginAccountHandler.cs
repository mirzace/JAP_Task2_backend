using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScreenplayApp.Application.Commands;
using ScreenplayApp.Application.Responses;
using ScreenplayApp.Core.Entities;
using ScreenplayApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScreenplayApp.Application.Handlers.CommandHandlers
{
    public class LoginAccountHandler : IRequestHandler<LoginAccountCommand, AccountResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly ITokenService _tokenService;

        public LoginAccountHandler(UserManager<AppUser> userManager, SignInManager<AppUser> singInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _tokenService = tokenService;
        }

        public async Task<AccountResponse> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            var userEntitiy = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == request.UserName.ToLower());

            if(userEntitiy == null) throw new ApplicationException("Invalid Username");

            var result = await _singInManager.CheckPasswordSignInAsync(userEntitiy, request.Password, false);
            if(!result.Succeeded) throw new ApplicationException("Wrong Credentials");

            var accountResponse = new AccountResponse
            {
                FirstName = userEntitiy.FirstName,
                LastName = userEntitiy.LastName,
                UserName = userEntitiy.UserName,
                Token = await _tokenService.CreateToken(userEntitiy)
            };

            return accountResponse;
        }
    }
}
