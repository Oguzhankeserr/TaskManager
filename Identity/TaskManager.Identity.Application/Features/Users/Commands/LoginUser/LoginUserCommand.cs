using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.CommonModels;
using TaskManager.Identity.Application.Features.Token.Commands;
using TaskManager.Identity.Application.Models;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Application.Features.Users.Commands.LoginUser
{
        public class LoginUserCommandRequest : IRequest<ActionResponse<TokenDto>>
        {
            public string UserNameOrEmail { get; set; }
            public string Password { get; set; }
        }


        public class LoginUserCommand : IRequestHandler<LoginUserCommandRequest, ActionResponse<TokenDto>>
        {
            readonly UserManager<AppUser> _userManager;
            readonly SignInManager<AppUser> _signInManager;
            readonly IMediator _mediator;

            public LoginUserCommand(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMediator mediator)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _mediator = mediator;
            }

            public async Task<ActionResponse<TokenDto>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
            {
                AppUser user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
                }
                if (user == null)
                {
                    //throw new NotFoundUserException("User name or Email incorrect");
                }

                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    return await _mediator.Send(new TokenCommandRequest { User = user });
                }
                else
                {
                    //throw new NotFoundUserException("password incorrect");
                }
                return null;

            }
        }
    }

