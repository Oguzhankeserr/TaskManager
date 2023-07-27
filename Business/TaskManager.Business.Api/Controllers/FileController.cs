using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Application.Features.Files;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResponse<TaskFile>> Upload([FromForm] UploadTaskFileCommandRequest uploadTaskFileRequest)
        {
            ActionResponse<TaskFile>response = await _mediator.Send(uploadTaskFileRequest);
            return response;
        }
    }

}
