using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.CommonModels;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Application.Features.Users.Commands.GetUser
{
    public class DeleteUserRequest : IRequest<ActionResponse<UserDto>>
    {
        public string UserId { get; set; }
    }

    public class DeleteUserCommand : IRequestHandler<DeleteUserRequest, ActionResponse<UserDto>>
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public DeleteUserCommand(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResponse<UserDto>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<UserDto> response = new();
            response.IsSuccessful = false;

            AppUser user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user != null)
            { 
                user.Status = false;
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                response.IsSuccessful = true;
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