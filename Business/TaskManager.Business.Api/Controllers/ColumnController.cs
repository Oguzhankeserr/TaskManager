using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManager.Business.Application.Features;
using TaskManager.Business.Application.Features.Column;
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
        readonly BusinessDbContext _businessDbContext;

        public ColumnController(IMediator mediator, BusinessDbContext businessDbContext)
        {
            _mediator = mediator;
            _businessDbContext = businessDbContext;
        }

        [HttpPost]
        public async Task<ActionResponse<Column>> CreateColumn(CreateColumnCommandRequest createColumnRequest)
        {
            ActionResponse<Column> response = await _mediator.Send(createColumnRequest);
            return response;
        }

        [HttpPost]
        public async Task<ActionResponse<Column>> UpdateColumn(UpdateColumnCommandRequest updateColumnRequest)
        {
            ActionResponse<Column> response = await _mediator.Send(updateColumnRequest);
            return response;
        }

        [HttpPost]
        public async Task<ActionResponse<Column>> DeleteColumn(DeleteColumnCommandRequest deleteColumnRequest)
        {
            ActionResponse<Column> response = await _mediator.Send(deleteColumnRequest);
            return response;
        }

        [HttpGet]
        public async Task<ActionResponse<List<Column>>> GetAllProjectColumns(GetProjectColumnsCommandRequest getProjectColumnsRequest)
        {
            ActionResponse<List<Column>> response = await _mediator.Send(getProjectColumnsRequest);
            return response;
        }
    }
}
