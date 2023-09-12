using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.Task
{
    public class GetUsersProjectsTasksRequest : IRequest<ActionResponse<List<TaskUserDto>>>
    {
        public string UserId { get; set; }
    }

    public class GetUsersProjectsTasksQuery : IRequestHandler<GetUsersProjectsTasksRequest, ActionResponse<List<TaskUserDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetUsersProjectsTasksQuery(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<TaskUserDto>>> Handle(GetUsersProjectsTasksRequest getTasksRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<TaskUserDto>> response = new();
            response.IsSuccessful = false;
            
            try
            {
                string query = @" SELECT t.id, t.name, t.projectid, t.priority, t.assigneeid, t.label FROM tasks t INNER JOIN projectusers pu ON t.projectid = pu.projectid
                                WHERE pu.userid = @UserId";
                var tasks = _businessDbContext.ExecuteQuery<TaskUserDto>(query, new { UserId = getTasksRequest.UserId });
                response.Data = tasks.ToList();
                response.IsSuccessful = true;

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
