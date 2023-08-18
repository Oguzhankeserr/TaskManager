using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using TaskManager.Business.Application.Features;
using TaskManager.Business.Application.Features.Task;
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

        [HttpPost]
        public async Task<ActionResponse<List<TaskUserDto>>> GetAllTaskForUser(GetAllTaskForUserCommandRequest getAllTaskForUserRequest)
        {
            return await _mediator.Send(getAllTaskForUserRequest);
        }

        [HttpGet]
        public async Task<ActionResponse<List<TaskDto>>> UnplannedTask([FromQuery]GetUnplannedTasksRequest unplannedTaskRequest)
        {
            return await _mediator.Send(unplannedTaskRequest);
        }

        [HttpGet]
        public async Task<ActionResponse<List<TaskDto>>> UnassignedTask([FromQuery] GetUnassignedTasksRequest unassignedTaskRequest)
        {
            return await _mediator.Send(unassignedTaskRequest);
        }



    }
}
