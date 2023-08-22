using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManager.Business.Application.Features.Task
{
    public class GetAllTaskQueryRequest : IRequest<ActionResponse<List<TaskListDto>>>
    {
        public int Id { get; set; } //projectId
    }

    public class GetAllTaskQuery : IRequestHandler<GetAllTaskQueryRequest, ActionResponse<List<TaskListDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetAllTaskQuery(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<TaskListDto>>> Handle(GetAllTaskQueryRequest getAllTaskCommandRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<TaskListDto>> response = new();
            response.IsSuccessful = false;

            string query = @" SELECT id, name, priority, columnid, createddate, updateddate, enddate AS DueDate, assigneeid, reporterid, isdone, CAST(createdbyuser AS varchar(50)) AS createdByUser
                            FROM tasks
                            WHERE projectid = @ProjectId AND status = true";
            try
            {
                var tasks = _businessDbContext.ExecuteQuery<TaskListDto>(query, new { ProjectId = getAllTaskCommandRequest.Id });
                response.Data = tasks.ToList();
                response.IsSuccessful = true;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccessful = false;
            }
            return response;
        }

    }
}
