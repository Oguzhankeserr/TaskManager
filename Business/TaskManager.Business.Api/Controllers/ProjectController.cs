 using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using TaskManager.Business.Application.Features;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly BusinessDbContext _businessDbContext;

        public ProjectController(IMediator mediator, BusinessDbContext businessDbContext)
        {
            _mediator = mediator;
            _businessDbContext = businessDbContext;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResponse<Project>> CreateProject(CreateProjectCommandRequest createProjectRequest)
        {

            ActionResponse<Project> response = await _mediator.Send(createProjectRequest);
            return response;
           
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResponse<Project>> UpdateProject(UpdateProjectCommandRequest updateProjectRequest)
        {
            ActionResponse<Project> response = await _mediator.Send(updateProjectRequest);
            return response;
        }

        [HttpPost] //todo Should the status of the columns also change to false when we delete the project? ASK!
        public async Task<ActionResponse<Project>> DeleteProject(DeleteProjectCommandRequest deleteProjectRequest)
        {
            ActionResponse<Project> response = await _mediator.Send(deleteProjectRequest);
            return response;
        }

        [HttpPost]
        public async Task<ActionResponse<Project>> GetProject(GetProjectCommandRequest getProjectRequest)
        {
            ActionResponse<Project> response = await _mediator.Send(getProjectRequest);
            return response;
        }

        [Authorize]
        [HttpGet]  
        public async Task<ActionResponse<List<ProjectDto>>> GetAllProjects()
        {
            ActionResponse<List<ProjectDto>> response = new();
            response.IsSuccessful = false;

            string userRole = User.FindFirstValue("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            if (!string.IsNullOrEmpty(userRole) && userRole.Equals("SuperAdmin", StringComparison.OrdinalIgnoreCase))
            {
                string query = "SELECT id, name, createddate, description FROM projects WHERE status = true";
                var projects = _businessDbContext.ExecuteQuery<ProjectDto>(query);

                response.Data = projects;
                response.IsSuccessful = true;
                return response;
            }


            string userId = User.FindFirstValue("UserId");
            if(userId != null) 
            {
                string query = "SELECT p.id, p.name, p.createddate, p.description FROM projectusers pu " +
                    "JOIN projects p ON pu.projectid = p.id WHERE pu.userid = @UserId  AND pu.status = true AND p.status = true";
                var projects = _businessDbContext.ExecuteQuery<ProjectDto>(query, new { UserId = userId });

                response.Data = projects;
                response.IsSuccessful = true;

                return response;
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = "User not found.";
                return response;
            }
            
        }

       

    }
}
