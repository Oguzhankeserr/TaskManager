using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.CommonModels;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandRequest : IRequest<ActionResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class RegisterUserCommand : IRequestHandler<RegisterUserCommandRequest, ActionResponse<UserDto>>
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public RegisterUserCommand(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResponse<UserDto>> Handle(RegisterUserCommandRequest registerRequest, CancellationToken cancellationToken)
        {
            ActionResponse<UserDto> response = new();
            UserDto userDto = new();
            response.IsSuccessful = false;

            var roleExists = await _roleManager.RoleExistsAsync(registerRequest.Role);

            if (roleExists)
            {
                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = registerRequest.UserName,
                    Name = registerRequest.Name,
                    Surname = registerRequest.Surname,
                    Email = registerRequest.Email,
                };

                IdentityResult result = await _userManager.CreateAsync(user, registerRequest.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, registerRequest.Role);
                    userDto.Username = registerRequest.UserName;
                    userDto.Id = user.Id;
                    userDto.Surname = user.Surname;
                    userDto.Name = registerRequest.Name;
                    userDto.Role = registerRequest.Role;
                    userDto.Email = registerRequest.Email;
                    response.Data = userDto;
                    response.IsSuccessful = true;
                }
                else
                {
                    foreach (var error in result.Errors)
                        response.Message += $"{error.Code} - {error.Description}\n";

                }
            }
            else
            {
                response.Message = "Role doesn't exist";
            }
            
            return response;
        }
    }
}
