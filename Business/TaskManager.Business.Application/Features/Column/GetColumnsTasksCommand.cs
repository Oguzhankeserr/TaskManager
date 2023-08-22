using MediatR;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Business.Application.Features
{
    public class GetColumnsTasksCommandRequest : IRequest<ActionResponse<List<ColumnTaskDto>>>
    {
        public int Id { get; set; }
    }

    public class GetColumnsTasksCommand : IRequestHandler<GetColumnsTasksCommandRequest, ActionResponse<List<ColumnTaskDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetColumnsTasksCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<ColumnTaskDto>>> Handle(GetColumnsTasksCommandRequest columnsTasksRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<ColumnTaskDto>> response = new();
            response.IsSuccessful = false;

            string columnsQuery = @"SELECT id, name, projectid FROM columns
                                WHERE projectid = @ProjectId AND status = true order by createddate";
            try
            {
                var columns = await _businessDbContext.Database.GetDbConnection().QueryAsync<ColumnTaskDto>(columnsQuery, new { ProjectId = columnsTasksRequest.Id });

                var columnIds = columns.Select(column => column.Id).ToArray();
                string taskQuery = "SELECT id, name, priority, columnid FROM tasks WHERE status = true AND tasks.columnid in (";
                foreach (var column in columnIds)
                {
                    taskQuery += "'" + column + "',";
                }
                taskQuery = taskQuery.TrimEnd(',');
                taskQuery += ")";

                if (columns.Any())
                {
                    var tasks = await _businessDbContext.Database.GetDbConnection().QueryAsync<TaskDto>(taskQuery);

                    foreach (var task in tasks)
                    {
                        var column = columns.FirstOrDefault(c => c.Id  == task.ColumnId);
                        if (column != null)
                        {
                            if (column.Tasks == null)
                            {
                                column.Tasks = new List<TaskDto>();
                            }
                            column.Tasks.Add(task);
                        }
                    }
                }

                foreach (var column in columns)
                {
                    if (column.Tasks == null)
                    {
                        column.Tasks = new List<TaskDto>();
                    }
                }

                response.Data = columns.ToList();
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
            
    

