﻿using Azure.Core;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManager.Business.Application.Features.Comment
{
    public class GetTaskCommentsQueryRequest : IRequest<ActionResponse<List<CommentDto>>>
    {
        public int TaskId { get; set; }
    }
    public class GetTaskCommentsQuery : IRequestHandler<GetTaskCommentsQueryRequest, ActionResponse<List<CommentDto>>>
    {
        private readonly BusinessDbContext _dbContext;

        public GetTaskCommentsQuery(BusinessDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResponse<List<CommentDto>>> Handle(GetTaskCommentsQueryRequest commentRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<CommentDto>> response = new();
            response.IsSuccessful = false;


            var sql = "SELECT id, comment, createddate, updateddate, createdbyuser, rewrite FROM comments WHERE taskid = @TaskId AND status = true";
            try
            {
                var selectedUsers =  _dbContext.ExecuteQuery<CommentDto>(sql, new { TaskId = commentRequest.TaskId });
                response.Data = selectedUsers.ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}


