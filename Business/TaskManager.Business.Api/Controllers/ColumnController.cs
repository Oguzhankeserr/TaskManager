using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManager.Business.Application.Features;
using TaskManager.Business.Application.Features.Column;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        readonly IMediator _mediator;

        public ColumnController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResponse<Column>> CreateColumn(CreateColumnCommandRequest createColumnRequest)
        {
            return await _mediator.Send(createColumnRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<Column>> UpdateColumn(UpdateColumnCommandRequest updateColumnRequest)
        {
            return await _mediator.Send(updateColumnRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<Column>> DeleteColumn(DeleteColumnCommandRequest deleteColumnRequest)
        {
            return await _mediator.Send(deleteColumnRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<List<ColumnDto>>> GetAllProjectColumns(GetProjectColumnsCommandRequest getProjectColumnsRequest)
        {
            return await _mediator.Send(getProjectColumnsRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<List<ColumnTaskDto>>> GetProjectColumnsTasks(GetColumnsTasksCommandRequest columnsTasksRequest)
        {
            return await _mediator.Send(columnsTasksRequest);
        }

        [HttpPost] 
        public async Task<ActionResponse<ColumnDto>> TransferColumnTasks(TransferColumnTaskCommandRequest transferTaskRequest)
        {
            return await _mediator.Send(transferTaskRequest);
        }

    }
}
