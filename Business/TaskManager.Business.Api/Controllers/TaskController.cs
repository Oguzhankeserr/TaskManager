using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System.Security.Claims;
using TaskManager.Business.Application.Features;
using TaskManager.Business.Application.Features.Task;
using TaskManager.Business.Application.Features.Task.Commands;
using TaskManager.Business.Application.Features.Task.Queries;
using TaskManager.Business.Application.Features.Task.Query;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly BusinessDbContext _businessDbContext;

        public TaskController(IMediator mediator, BusinessDbContext businessDbContext)
        {
            _mediator = mediator;
            _businessDbContext = businessDbContext;
        }

        [HttpPost]
        public async Task<ActionResponse<Domain.Entities.Task>> CreateTask(CreateTaskCommandRequest createTaskRequest)//[FromForm]
        {
            ActionResponse<Domain.Entities.Task> response = await _mediator.Send(createTaskRequest);
            return response;
        }

		[HttpPost]
        public async Task<ActionResponse<List<TaskListDto>>> GetAllProjectTask(GetAllTaskQueryRequest getAllTaskRequest)
        {
            return await _mediator.Send(getAllTaskRequest);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResponse<List<TaskUserDto>>> GetUsersProjectsTasks()
        {
            string userId = User.FindFirstValue("UserId");
            return await _mediator.Send(new GetUsersProjectsTasksRequest{ UserId = userId});
        }

        [HttpPost]
        public async Task<ActionResponse<Domain.Entities.Task>>GetTaskById(GetTaskByIdCommandRequest getTaskByIdRequest)
        {
            return await _mediator.Send(getTaskByIdRequest); 
        }

        [HttpPost]
        public async Task<ActionResponse<Domain.Entities.Task>>UpdateTask(UpdateTaskCommandRequest updateTaskRequest)
        {
            return await _mediator.Send(updateTaskRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<Domain.Entities.Task>>UpdateTaskColumnId(UpdateTaskColumnIdCommandRequest updateTaskColumnIdRequest)
        {
            return await _mediator.Send(updateTaskColumnIdRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<Domain.Entities.Task>>DeleteTask(DeleteTaskCommandRequest deleteTaskRequest)
        {
            return await _mediator.Send(deleteTaskRequest);
        }

        [HttpGet]
        public async Task<ActionResponse<List<TaskDto>>> GetAllProjectTaskForUser([FromQuery] GetUserProjectTaskQuery query)
        {
            //return await _mediator.Send(new GetUserProjectTaskQuery { UserId = userId, ProjectId = projectId });
            return await _mediator.Send(query);

            //yeni request 
        }

      


    }
}
