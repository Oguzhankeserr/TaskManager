using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using TaskManager.CommonModels.Repositories;

namespace TaskManager.Business.Application.Features.Comment
{
    public class CreateCommentCommandRequest : IRequest<ActionResponse<CommentDto>>
    {
        public int Id { get; set; }
        public string Comment { get; set; }
    }

    public class CreateCommentCommand : IRequestHandler<CreateCommentCommandRequest, ActionResponse<CommentDto>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly IUserInfoRepository _userInfoRepository;
        readonly GenericService<Comments> _genericService;

        public CreateCommentCommand(BusinessDbContext businessDbContext, IUserInfoRepository userInfoRepository, GenericService<Comments> genericService)
        {
            _businessDbContext = businessDbContext;
            _userInfoRepository = userInfoRepository;
            _genericService = genericService;
        }

        public async Task<ActionResponse<CommentDto>> Handle(CreateCommentCommandRequest createCommendRequest, CancellationToken cancellationToken)
        {
            ActionResponse<CommentDto> response = new();
            response.IsSuccessful = true;

            Comments comment = new();
            comment.TaskId = createCommendRequest.Id;
            comment.Comment = createCommendRequest.Comment;
            comment.CreatedByUser = _userInfoRepository.User.UserId;
            comment.CreatedDate = DateTime.UtcNow;
            comment.Status = true;
            comment.Rewrite = false;
            //comment.UpdatedDate 

            //_businessDbContext.Comments.AddAsync(comment);
            //_businessDbContext.SaveChangesAsync();
            _genericService.AddAsync(comment);

            response.IsSuccessful = true;
            return response;
        }
    }
}
