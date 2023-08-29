using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.Task
{
    public class GetTasksQueryRequest : IRequest<ActionResponse<List<TaskUserDto>>>
    {
        public string UserId { get; set; }
    }

    public class GetTasksQuery : IRequestHandler<GetTasksQueryRequest, ActionResponse<List<TaskUserDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetTasksQuery(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<TaskUserDto>>> Handle(GetTasksQueryRequest getTasksRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<TaskUserDto>> response = new();
            response.IsSuccessful = false;

            string projectQuery = @"SELECT projectid FROM projectusers WHERE userid = @UserId AND status = true";
            try
            {
                var projectIds = _businessDbContext.ExecuteQuery<int>(projectQuery, new { UserId = getTasksRequest.UserId });

                if (projectIds.Any())
                {
                    string taskQuery = @"SELECT id, name, priority, assigneeid, label
                                     FROM tasks
                                     WHERE status = true AND tasks.projectid in (";
                    foreach(var id in projectIds)
                    {
                        taskQuery += "'" + id + "',";
                    }
                    taskQuery = taskQuery.TrimEnd(',');
                    taskQuery += ")";

                    var tasks = _businessDbContext.ExecuteQuery<TaskUserDto>(taskQuery);

                    response.Data = tasks.ToList();
                    response.IsSuccessful = true;
                }
                else
                {
                    response.Message = "No projects found for the user.";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccessful = false;
            }
            return response;
        }
    }

}






