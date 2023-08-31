using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features
{
    public class DeleteUserFromProjectCommandRequest : IRequest<ActionResponse<ProjectUserDto>>
    {
        public int ProjectId { get; set; }
        public List<string> Users { get; set; }
    }

    public class DeleteUserFromProjectCommand : IRequestHandler<DeleteUserFromProjectCommandRequest, ActionResponse<ProjectUserDto>>
    {
        readonly BusinessDbContext _businessDbContext;

        public DeleteUserFromProjectCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<ProjectUserDto>> Handle(DeleteUserFromProjectCommandRequest deleteUserRequest, CancellationToken cancellationToken)
        {
            ActionResponse<ProjectUserDto> response = new();
            response.IsSuccessful = false;

            string query = "UPDATE projectusers SET status = false WHERE projectusers.UserId in (";
            foreach (var user in deleteUserRequest.Users)
            {
                query += "'" + user + "',";
            }
            query = query.TrimEnd(',');
            query += ")";

            await _businessDbContext.Database.ExecuteSqlRawAsync(query);

            response.IsSuccessful = true;
            response.Message = "Users deleted from the project successfully.";

            return response;

        }
    }
}

