using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Log.Dto;
using TaskManager.Business.LogService.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.LogService.Application.Queries
{
    public class GetUserLogQuery : IRequest<ActionResponse<List<LogUserDto>>>
    {
        public List<int> ProjectIds { get; set; }
        public string UserId { get; set; }
    }

    public class GetUserLogQueryHandler : IRequestHandler<GetUserLogQuery, ActionResponse<List<LogUserDto>>>
    {
        readonly LogDbContext _logDbContext;

        public GetUserLogQueryHandler(LogDbContext logDbContext)
        {
            _logDbContext = logDbContext;
        }

        public async Task<ActionResponse<List<LogUserDto>>> Handle(GetUserLogQuery request, CancellationToken cancellationToken)
        {
            ActionResponse<List<LogUserDto>> response = new();
            response.IsSuccessful = false;
            
            try
            {
                string query = "Select tablename, fieldname, oldvalue, newvalue, actiondate, projectid FROM logs WHERE userid = @userId AND logs.projectId in (";
                foreach(var projectId in request.ProjectIds)
                {
                    query += "'" + projectId + "',";
                }
                query = query.TrimEnd(',');
                query += ")";
                var logs = _logDbContext.ExecuteQuery<LogUserDto>(query, new { userId = request.UserId });
                response.Data = logs.ToList();
                response.IsSuccessful = true;

            }
            catch(Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
