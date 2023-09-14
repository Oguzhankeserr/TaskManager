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
using Npgsql;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace TaskManager.Business.LogService.Application.Queries
{
    public class GetUserLogQuery : IRequest<ActionResponse<PagedResult<LogUserDto>>>
    {
        public List<int> ProjectIds { get; set; }
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }


    public class GetUserLogQueryHandler : IRequestHandler<GetUserLogQuery, ActionResponse<PagedResult<LogUserDto>>>
    {
        readonly LogDbContext _logDbContext;

        public GetUserLogQueryHandler(LogDbContext logDbContext)
        {
            _logDbContext = logDbContext;
        }

        public async Task<ActionResponse<PagedResult<LogUserDto>>> Handle(GetUserLogQuery request, CancellationToken cancellationToken)
        {
            ActionResponse<PagedResult<LogUserDto>> response = new();
            response.IsSuccessful = false;

            try
            {
                var baseQuery = _logDbContext.Logs
                    .Where(log => log.UserId == request.UserId && request.ProjectIds.Contains(log.ProjectId));

                var totalCount = await baseQuery.CountAsync(cancellationToken); 

                int skip = (request.PageNumber - 1) * request.PageSize;
                int take = request.PageSize;

                var query = baseQuery
                    .OrderByDescending(log => log.ActionDate)
                    .Skip(skip)
                    .Take(take)
                    .Select(log => new LogUserDto
                    {
                        TableName = log.TableName,
                        FieldName = log.FieldName,
                        OldValue = log.OldValue,
                        NewValue = log.NewValue,
                        ActionDate = log.ActionDate,
                        ProjectId = log.ProjectId.ToString()
                    });

                var logs = await query.ToListAsync(cancellationToken);

                response.Data = new PagedResult<LogUserDto>(logs, request.PageNumber, request.PageSize, totalCount);

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
