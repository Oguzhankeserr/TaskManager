using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
using TaskManager.CommonModels.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskManager.Business.Application.Features.ProjectUser.Commands
{
    public class DeleteUserFromProjectTaskRequest : IRequest<ActionResponse<ProjectUserDto>>
    {
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string AssigneeId { get; set; }
        public string ReporterId { get; set; }
    }

    public class DeleteUserFromProjectTaskCommand : IRequestHandler<DeleteUserFromProjectTaskRequest, ActionResponse<ProjectUserDto>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly GenericService<Domain.Entities.Task> _taskGenericService;
        readonly IUserInfoRepository _userInfoRepository;

        public DeleteUserFromProjectTaskCommand(BusinessDbContext businessDbContext, GenericService<Domain.Entities.Task> taskGenericService,
            IUserInfoRepository userInfoRepository)
        {
            _businessDbContext = businessDbContext;
            _taskGenericService = taskGenericService;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<ActionResponse<ProjectUserDto>> Handle(DeleteUserFromProjectTaskRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<ProjectUserDto> response = new();
            response.IsSuccessful = false;

            try
            {
                //string taskQuery = "Select * from tasks where projectid = @ProjectId AND status = true AND (assigneeid = @UserId OR reporterid = @UserId) ";
                //var tasks = _businessDbContext.ExecuteQuery<Domain.Entities.Task>(taskQuery, new { ProjectId = request.ProjectId, UserId = request.UserId });

                var tasks = _businessDbContext.Tasks.Where(p => p.ProjectId == request.ProjectId && (p.AssigneeId == request.UserId || p.ReporterId == request.UserId) && p.Status).ToList();

                foreach (var task in tasks)
                {
                    if (task.ReporterId == request.UserId)
                    {
                        task.ReporterId = request.ReporterId;
                    }
                    if (task.AssigneeId == request.UserId)
                    {
                        task.AssigneeId = request.AssigneeId;
                    }
                    task.UpdatedByUser = _userInfoRepository.User.UserId;
                    task.UpdatedDate = DateTime.UtcNow;
                }
                _taskGenericService.UpdateRangeList(tasks);
                response.IsSuccessful = true;
                Thread.Sleep(1000);

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
            }

            return  response;

        }
    }

}
/*
 string updateTasksQuery = "UPDATE tasks SET assigneeId = @NewAssigneeId, reporterId = @NewReporterId WHERE assigneeId = @UserId OR reporterId = @UserId";
                
                var tasks = _businessDbContext.Database.ExecuteSqlRawAsync(updateTasksQuery, new
                {
                    NewAssigneeId = request.AssigneeId,
                    NewReporterId = request.ReporterId,
                    UserId = request.UserId
                });

                string query = "UPDATE projectusers SET status = false WHERE projectid = @ProjectId AND userid = @UserId ";

                await _businessDbContext.Database.ExecuteSqlRawAsync(query,
                    new NpgsqlParameter("@ProjectId", request.ProjectId),
                    new NpgsqlParameter("@UserId", request.UserId));

                
                response.IsSuccessful = true;
                response.Message = "Users deleted from the project successfully.";
 
 
 */