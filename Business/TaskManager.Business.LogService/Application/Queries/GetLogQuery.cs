using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Log.Dto;
using TaskManager.Business.LogService.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.LogService.Application.Queries
{
    public class GetLogQuery : IRequest<ActionResponse<List<LogDto>>>
    {
        public string TableName { get; set; }
        public string TableId { get; set; }
    }

    public class GetLogQueryHandler : IRequestHandler<GetLogQuery, ActionResponse<List<LogDto>>>
    {
        readonly LogDbContext _logDbContext;

        public GetLogQueryHandler(LogDbContext logDbContext)
        {
            _logDbContext = logDbContext;
        }

        public async Task<ActionResponse<List<LogDto>>> Handle(GetLogQuery request, CancellationToken cancellationToken)
        {
            ActionResponse<List<LogDto>> response = new();
            response.IsSuccessful = false;
          
            try
            {
                string query = "Select fieldname, oldvalue, newvalue, actiondate, userid FROM logs WHERE tablename = @tablename AND tableid = @tableid::integer";
                var logs = _logDbContext.ExecuteQuery<LogDto>(query, new { tablename = request.TableName, tableid = request.TableId });
                response.Data = logs.ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
