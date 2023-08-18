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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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

            string sql = @" SELECT c.projectid, c.name, c.id,
                             t.id AS Id, t.name AS Name, t.priority AS Priority, t.columnid AS ColumnId
                             FROM columns c 
                             LEFT JOIN tasks t ON c.id = t.columnid 
                             WHERE c.projectid = @ProjectId AND c.status = true AND t.status = true ";
            try
            {
                var queryResult = await _businessDbContext.Database.GetDbConnection().QueryAsync<ColumnTaskDto, TaskDto, ColumnTaskDto>(
                     sql, (column, task) =>
                     {
                         column.Tasks ??= new List<TaskDto>();
                         if (task != null)
                         {
                             column.Tasks.Add(task);
                         }
                         return column;
                     },
                     new { ProjectId = columnsTasksRequest.Id }, splitOn: "Id,Id");

                var groupTasks = queryResult.GroupBy(column => column.Id).Select(group => new ColumnTaskDto
                {
                    ProjectId = group.First().ProjectId,
                    Name = group.First().Name,
                    Id = group.Key,
                    Tasks = group.SelectMany(column => column.Tasks).ToList()
                }
                ).ToList();
                response.Data = groupTasks;
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
            
    

