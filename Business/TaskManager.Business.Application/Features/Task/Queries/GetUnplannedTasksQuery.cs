using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.Task.Query
{
    public class GetUnplannedTasksRequest : IRequest<ActionResponse<List<TaskDto>>>
    {
        public int Id { get; set; }
    }
    public class GetUnplannedTasksQuery : IRequestHandler<GetUnplannedTasksRequest, ActionResponse<List<TaskDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetUnplannedTasksQuery(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<TaskDto>>> Handle(GetUnplannedTasksRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<List<TaskDto>> response = new();
            response.IsSuccessful = false;

            string query = "SELECT id, name, columnid, priority FROM tasks WHERE projectid = @Id AND enddate = '-infinity' AND status = true";

            try
            {
                var selectedTasks = _businessDbContext.ExecuteQuery<TaskDto>(query, new { request.Id });
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
