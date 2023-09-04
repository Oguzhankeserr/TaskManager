using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using TaskManager.Business.Domain.Entities;

namespace TaskManager.Business.Application.Features.ProjectUser
{
    public class GetSelectedUsersForProjectQuery : IRequest<ActionResponse<List<ProjectUserDto>>>
    {
        public int ProjectId { get; set; }
    }

    public class GetSelectedUsersForProjectQueryHandler : IRequestHandler<GetSelectedUsersForProjectQuery, ActionResponse<List<ProjectUserDto>>>
    {
        private readonly string _connectionString;

        public GetSelectedUsersForProjectQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TaskManagerBusinessConnection");
        }

        public async Task<ActionResponse<List<ProjectUserDto>>> Handle(GetSelectedUsersForProjectQuery request, CancellationToken cancellationToken)
        {
            ActionResponse<List<ProjectUserDto>> response = new();
            response.IsSuccessful = false;

          
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT id, projectid, userId FROM projectusers WHERE projectid = @ProjectId AND status = true";
                try 
                {
                    var selectedUsers = await connection.QueryAsync<ProjectUserDto>(sql, new { ProjectId = request.ProjectId });
                    response.Data = selectedUsers.ToList();
                    response.IsSuccessful = true;
                }
                catch (Exception ex)
                {
                    response.Message = ex.Message; 
                }
            }

            return response;
        }
    }



}
