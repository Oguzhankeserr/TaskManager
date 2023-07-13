﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using TaskManager.Business.Application.Features;
using TaskManager.Business.Application.Features.Task;
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
        public async Task<ActionResponse<Domain.Entities.Task>> CreateTask(CreateTaskCommandRequest createTaskRequest)
        {
            ActionResponse<Domain.Entities.Task> response = await _mediator.Send(createTaskRequest);
            return response;
        }

        //[HttpGet]
        //public async Task<ActionResponse<List<Domain.Entities.Task>>> GetTask(GetTaskCommandRequest getTaskRequest)
        //{ 
        //   return await _mediator.Send(getTaskRequest);
        //}

        [HttpGet]
        public async Task<ActionResponse<Domain.Entities.Task>>GetTaskById([FromQuery] GetTaskByIdCommandRequest getTaskByIdRequest)
        {
            return await _mediator.Send(getTaskByIdRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<Domain.Entities.Task>>UpdateTaks(UpdateTaskCommandRequest updateTaskRequest)
        {
            return await _mediator.Send(updateTaskRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<Domain.Entities.Task>>DeleteTask(DeleteTaskCommandRequest deleteTaskRequest)
        {
            return await _mediator.Send(deleteTaskRequest);
        }
    }
}
