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
    public class GetUnassignedTasksRequest : IRequest<ActionResponse<List<TaskDto>>>
    {
        public int Id { get; set; }
    }

    public class GetUnassignedTasksQuery : IRequestHandler<GetUnassignedTasksRequest, ActionResponse<List<TaskDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetUnassignedTasksQuery(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<TaskDto>>> Handle(GetUnassignedTasksRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<List<TaskDto>> response = new();
            response.IsSuccessful = false;

            string query = "SELECT id, name, columnid, priority FROM tasks WHERE projectid = @Id AND assigneeid = @User AND status = true";

            try
            {
                var selectedTasks = _businessDbContext.ExecuteQuery<TaskDto>(query, new { Id = request.Id, User = "unassigned" });
                response.Data = selectedTasks;
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

