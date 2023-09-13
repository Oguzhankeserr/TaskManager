using MediatR;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.CommonModels;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Application.Features.Users.Commands.GetUser
{
    public class UpdateUserRequest : IRequest<ActionResponse<UserDto>>
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }

    public class UpdateUserCommand : IRequestHandler<UpdateUserRequest, ActionResponse<UserDto>>
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public UpdateUserCommand(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResponse<UserDto>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<UserDto> response = new();
            response.IsSuccessful = false;

            AppUser user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            {
                user.Name = request.Name;
                user.Surname = request.Surname;
                user.UserName = request.Username;
                user.Email = request.Email;
                //user.Status = request.Status;
                
                if (await _roleManager.RoleExistsAsync(request.Role))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, userRoles);
                    await _userManager.AddToRoleAsync(user, request.Role);
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        response.IsSuccessful = true;
                    }
                    else
                    {
                        response.IsSuccessful = false;
                        response.Message = "User cannot updated.";
                    }

                }
                return response;
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = "User cannot found.";
            }
            return response;
        }

    }
}