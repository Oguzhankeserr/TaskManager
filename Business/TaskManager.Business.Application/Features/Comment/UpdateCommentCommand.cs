using Azure.Core;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.Comment
{
    public class UpdateCommentRequest : IRequest<ActionResponse<CommentDto>>
    {
        public int Id { get; set; }
        public string Comment { get; set; }
    }

    public class UpdateCommentCommand : IRequestHandler<UpdateCommentRequest, ActionResponse<CommentDto>>
    {
        private readonly string _connectionString;

        public UpdateCommentCommand(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TaskManagerBusinessConnection");
        }

        public async Task<ActionResponse<CommentDto>> Handle(UpdateCommentRequest updateComment,CancellationToken cancellationToken)
        {
            ActionResponse<CommentDto> response = new();
            response.IsSuccessful = false;
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    string query = "UPDATE comments SET comment = @NewComment, rewrite = true, updateddate=@updatedDate WHERE id = @Id;";
                    await connection.ExecuteAsync(query,
                        new { NewComment = updateComment.Comment, Id = updateComment.Id, updateddate = DateTime.UtcNow });

                    response.IsSuccessful = true;
                }
                catch (Exception ex)
                {
                    response.IsSuccessful = false;
                    response.Message = ex.Message;

                }
            }
            return response;

        }
    }
}
