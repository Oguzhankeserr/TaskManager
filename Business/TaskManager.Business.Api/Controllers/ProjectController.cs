using MediatR;
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

        //todo [Authorize] for admin
        [HttpPost]
        public async Task<ActionResponse<Project>> CreateProject(CreateProjectCommandRequest createProjectRequest)
        {
            ActionResponse<Project> response = await _mediator.Send(createProjectRequest);
            return response;
        }

        [HttpPost]
        public async Task<ActionResponse<Project>> UpdateProject(UpdateProjectCommandRequest updateProjectRequest)
        {
            ActionResponse<Project> response = await _mediator.Send(updateProjectRequest);
            return response;
        }

        [HttpPost]
        public async Task<ActionResponse<Project>> DeleteProject(DeleteProjectCommandRequest deleteProjectRequest)
        {
            ActionResponse<Project> response = await _mediator.Send(deleteProjectRequest);
            return response;
        }


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
