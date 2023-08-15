using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using TaskManager.Business.Application.Features.Comment;
using TaskManager.Business.Domain.Dtos;
using TaskManager.CommonModels;

namespace TaskManager.Business.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResponse<CommentDto>> CreateComment(CreateCommentCommandRequest createCommentRequest)
        {
            return await _mediator.Send(createCommentRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<CommentDto>> UpdateComment(UpdateCommentRequest updateCommentRequest)
        {
            return await _mediator.Send(updateCommentRequest);
        }

        [HttpGet]
        public async Task<ActionResponse<List<CommentDto>>> GetTaskComments([FromQuery]GetTaskCommentsQueryRequest getTaskRequest)
        {
            return await _mediator.Send(getTaskRequest);
        }

        [HttpPost]
        public async Task<ActionResponse<CommentDto>> DeleteComment(DeleteCommentRequest deleteRequest)
        {
            return await _mediator.Send(deleteRequest);
        }
    }
}
