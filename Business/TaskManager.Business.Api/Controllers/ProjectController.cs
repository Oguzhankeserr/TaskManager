using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManager.Business.Application.Features;
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

        [HttpGet]
        public async Task<ActionResponse<Project>> GetProject(GetProjectCommandRequest getProjectRequest)
        {
            ActionResponse<Project> response = await _mediator.Send(getProjectRequest);
            return response;
        }

        [Authorize]
        [HttpGet]  //admin için özelleştirme gerekli mi? Projenin adminleri?
        public async Task<ActionResponse<List<Project>>> GetAllProjects()
        {
            List<Project> projects = _businessDbContext.Projects.Where(x => x.Status == true).ToList();
            ActionResponse<List<Project>> projectResponse = new();
            projectResponse.Data = projects;
            projectResponse.IsSuccessful = true;
            return projectResponse;
        }

    }
}
