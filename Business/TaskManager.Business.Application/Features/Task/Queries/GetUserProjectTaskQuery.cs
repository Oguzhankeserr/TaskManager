using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;


namespace TaskManager.Business.Application.Features.Task.Queries
{
    public class GetUserProjectTaskQuery: IRequest<ActionResponse<List<TaskDto>>>
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
    }

    public class GetUserProjectTaskQueryHandler : IRequestHandler<GetUserProjectTaskQuery, ActionResponse<List<TaskDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetUserProjectTaskQueryHandler(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        async public Task<ActionResponse<List<TaskDto>>> Handle(GetUserProjectTaskQuery request, CancellationToken cancellationToken)
        {
            ActionResponse<List<TaskDto>> response = new();
            response.IsSuccessful = false;

            try
            {
                string taskQuery = "Select id, name, columnid, priority, label from tasks where projectid = @ProjectId AND status = true AND (assigneeid = @UserId OR reporterid = @UserId) ";
                var tasks = _businessDbContext.ExecuteQuery<TaskDto>(taskQuery,  new { ProjectId = request.ProjectId, UserId = request.UserId });
                response.Data = tasks.ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
            }

            response.IsSuccessful = true;
            return response;
        }
    }
}
