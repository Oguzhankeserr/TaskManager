using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Log.Dto;
using TaskManager.Business.LogService.Application.Queries;
using TaskManager.CommonModels;

namespace TaskManager.Business.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        readonly IMediator _mediator;

        public LogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResponse<List<LogDto>>> GetLogs([FromQuery] string TableName, [FromQuery] string TableId)
        {
            var parameters = new GetLogQuery
            {
                TableName = TableName,
                TableId = TableId
            };
            ActionResponse<List<LogDto>> response = await _mediator.Send(parameters);
            return response;
        }
        [HttpGet]
        public async Task<ActionResponse<PagedResult<LogUserDto>>> GetUserLogs([FromQuery] List<int> ProjectIds, [FromQuery] string UserId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var parameters = new GetUserLogQuery
            {
                ProjectIds = ProjectIds.ToList(),
                UserId = UserId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            ActionResponse<PagedResult<LogUserDto>> response = await _mediator.Send(parameters);
            return response;
        }

    }
}
