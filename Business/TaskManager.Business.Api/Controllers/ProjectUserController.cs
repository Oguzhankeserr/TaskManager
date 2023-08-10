using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManager.Business.Application.Features;
using TaskManager.Business.Application.Features.ProjectUser;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProjectUserController : ControllerBase
    {
        readonly IMediator _mediator;

        public ProjectUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResponse<ProjectUserDto>> AddUserToProject(AddUserToProjectCommandRequest addUserRequest)
        {
			return await _mediator.Send(addUserRequest);
		}

        [HttpPost]
        public async Task<ActionResponse<ProjectUserDto>> DeleteUserFromProject(DeleteUserFromProjectCommandRequest deleteUserRequest)
        {
            return await _mediator.Send(deleteUserRequest);
        }

        [Authorize]
        [HttpPost] 
        public async Task<ActionResponse<List<UserDto>>> GetAllProjectUsers(GetAllProjectUsersCommandRequest getAllProjectsRequest)
        {
            return await _mediator.Send(getAllProjectsRequest);
        }

        [HttpGet]
        public async Task<ActionResponse<List<ProjectUserDto>>> GetSelectedUsersForProject([FromQuery]GetSelectedUsersForProjectQuery selectedUserQuery)
        {
            ActionResponse < List <ProjectUserDto>> res = await _mediator.Send(selectedUserQuery);
            return res;
        }



    }
}
