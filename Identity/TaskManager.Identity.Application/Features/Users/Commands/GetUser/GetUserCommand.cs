using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.CommonModels;
using TaskManager.Identity.Application.Models;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Application.Features.Users.Commands.GetUser
{
    public class GetUserCommandRequest : IRequest<ActionResponse<AppUser>>
    {
        public string Id { get; set; }
    }

    public class GetUserCommand : IRequestHandler<GetUserCommandRequest, ActionResponse<AppUser>>
    {
        readonly UserManager<AppUser> _userManager;

        public GetUserCommand(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ActionResponse<AppUser>> Handle(GetUserCommandRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<AppUser> response = new();
            AppUser user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                response.IsSuccessful = false;
                response.Message = "User Not Found";
            }
            response.IsSuccessful = true;
            response.Data = user;

            return response;
        }
    }
}
