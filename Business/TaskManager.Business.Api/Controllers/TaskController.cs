using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Application.Features;
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
    }
}
