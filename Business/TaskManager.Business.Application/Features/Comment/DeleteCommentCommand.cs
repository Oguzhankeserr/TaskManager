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
    public class DeleteCommentRequest : IRequest<ActionResponse<CommentDto>>
    {
        public int Id { get; set; }
    }

    public class DeleteCommentCommand : IRequestHandler<DeleteCommentRequest, ActionResponse<CommentDto>>
    {
        //readonly BusinessDbContext _businessDbContext;
        private readonly string _connectionString;

        public DeleteCommentCommand(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TaskManagerBusinessConnection");
        }

        public async Task<ActionResponse<CommentDto>> Handle(DeleteCommentRequest deleteRequest, CancellationToken cancellationToken)
        {
            ActionResponse<CommentDto> response = new();
            response.IsSuccessful = false;
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    string query = "UPDATE comments SET status = false WHERE id = @Id;";
                    await connection.ExecuteAsync(query, new { Id = deleteRequest.Id });

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
