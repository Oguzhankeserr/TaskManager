using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommand : IRequestHandler<RegisterUserCommandRequest, RegisterUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;

        public RegisterUserCommand(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<RegisterUserCommandResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
            }, request.Password);

            RegisterUserCommandResponse response = new()
            {
                IsSuccessful = result.Succeeded,
            };

            if (result.Succeeded)
                response.Message = "User created.";
            else
            {
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";
            }
            return response;
        }
    }
}
