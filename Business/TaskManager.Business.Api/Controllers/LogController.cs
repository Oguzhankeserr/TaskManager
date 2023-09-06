using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
