using MediatR;
using Microsoft.AspNetCore.Identity;
using ScreenplayApp.Application.Commands;
using ScreenplayApp.Application.Mapper;
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
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, AccountResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly ITokenService _tokenService;

        public CreateAccountHandler(UserManager<AppUser> userManager, SignInManager<AppUser> singInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _tokenService = tokenService;
        }
        public async Task<AccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var userEntitiy = ScreenplayAppMapper.Mapper.Map<AppUser>(request);
            if (userEntitiy is null)
            {
                throw new ApplicationException("Issue with mapper");
            }

            userEntitiy.UserName = request.UserName.ToLower();

            var result = await _userManager.CreateAsync(userEntitiy, request.Password);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Issue with creating the user account!");
            }

            var roleResult = await _userManager.AddToRoleAsync(userEntitiy, "Consumer");
            if (!roleResult.Succeeded)
            {
                throw new ApplicationException("Issue with assigning a role to the user!");
            }

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
