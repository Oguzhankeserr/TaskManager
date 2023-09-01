using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Application.Features.Files;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using System.Net.Http.Headers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManager.Business.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class FileController : ControllerBase
    {
        readonly IMediator _mediator;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly BusinessDbContext _businessDbContext;


        public FileController(IMediator mediator, IWebHostEnvironment hostingEnvironment, BusinessDbContext businessDbContext)
        {
            _mediator = mediator;
            _hostingEnvironment = hostingEnvironment;
            _businessDbContext = businessDbContext;
            
            
        }

        [HttpPost]
        public async Task<ActionResponse<TaskFile>> Upload([FromForm] UploadTaskFileCommandRequest uploadTaskFileRequest)
        {
            ActionResponse<TaskFile> response = await _mediator.Send(uploadTaskFileRequest);
            return response;
        }

        [HttpPost]
        public async Task<ActionResponse<TaskFile>> UploadWithout([FromQuery]int taskId)
        {
            var x = Request.Form;
            List<IFormFile> file = Request.Form.Files.ToList();
            
                var command = new UploadTaskFileCommandRequest
                {
                    Files = file,
                    TaskId = taskId
                };
                ActionResponse<TaskFile> response = await _mediator.Send(command);
            
            
            
            return response;
        }

        [HttpPost]
        public async Task<ActionResponse<UserFile>> AddFile([FromQuery] string userId)
        {
            var x = Request.Form;

            List<IFormFile> file = Request.Form.Files.ToList();


            var c = new UploadUserFileCommand
            {
                Files = file,
                UserId = userId
            };
            ActionResponse<UserFile> response = await _mediator.Send(c);
            return response;

            
        }

        [HttpPost]
        public async Task<List<string>> GetFile(GetUserFileRequest getUserFileRequest)
        {
            return await _mediator.Send(getUserFileRequest);
            
            
        }

        [HttpPost]
        public async Task<List<string>> GetFileForTask(GetTaskFileRequest getTaskFileRequest)
        {
            return await _mediator.Send(getTaskFileRequest);


        }

    }

}
