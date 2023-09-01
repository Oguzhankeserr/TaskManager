﻿ using MediatR;
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

        [Authorize(Roles = "Admin")]
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

            /*
             
                 List<ProjectDto> projects = await _businessDbContext.ProjectUsers
                .Where(p => p.UserId == userId && p.Status == true)
                .Join(
                    _businessDbContext.Projects.Where(pr => pr.Status == true),
                    projectUser => projectUser.ProjectId,
                    project => project.Id,
                    (projectUser, project) => new ProjectDto
                    {
                        Id = project.Id,
                        Name = project.Name,
                        CreatedDate = project.CreatedDate,
                    }
                )
                .ToListAsync();
             */

            string userId = User.FindFirstValue("UserId");
            if(userId != null) 
            {
                string query = "SELECT p.id, p.name, p.createddate, p.description FROM projectusers pu " +
                    "JOIN projects p ON pu.projectid = p.id WHERE pu.userid = @UserId  AND pu.status = true AND p.status = true";
                var projects = _businessDbContext.ExecuteQuery<ProjectDto>(query, new { UserId = userId });

                response.Data = projects;
                response.IsSuccessful = true;
            }
            
            return response;
        }

       

    }
}
