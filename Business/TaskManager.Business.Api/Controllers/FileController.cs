using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Application.Features.Files;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using System.Net.Http.Headers;

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

        //[HttpPost]
        //[DisableRequestSizeLimit]
        //public async Task<IActionResult> UploadFile()
        //{
        //    if (!Request.Form.Files.Any())
        //        return BadRequest("No files found in the request");

        //    if (Request.Form.Files.Count > 1)
        //        return BadRequest("Cannot upload more than one file at a time");

        //    if (Request.Form.Files[0].Length <= 0)
        //        return BadRequest("Invalid file length, seems to be empty");

        //    try
        //    {
        //        string webRootPath = _hostingEnvironment.WebRootPath;
        //        string uploadsDir = Path.Combine(webRootPath, "uploads");

        //        wwwroot / uploads /
        //        if (!Directory.Exists(uploadsDir))
        //            Directory.CreateDirectory(uploadsDir);

        //        IFormFile file = Request.Form.Files[0];
        //        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //        string fullPath = Path.Combine(uploadsDir, fileName);

        //        var buffer = 1024 * 1024;
        //        using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer, useAsync: false);
        //        await file.CopyToAsync(stream);
        //        await stream.FlushAsync();

        //        string location = $"images/{fileName}";

        //        var result = new
        //        {
        //            message = "Upload successful",
        //            url = location

        //        };


        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Upload failed: " + ex.Message);
        //    }
        //}
    }

}
