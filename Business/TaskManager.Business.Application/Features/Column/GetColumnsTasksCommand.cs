﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features
{
    public class GetColumnsTasksCommandRequest : IRequest<ActionResponse<List<ColumnTaskDto>>>
    {
        public int ProjectId { get; set; }
    }

    public class GetColumnsTasksCommand : IRequestHandler<GetColumnsTasksCommandRequest, ActionResponse<List<ColumnTaskDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetColumnsTasksCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        //todo 
        public async Task<ActionResponse<List<ColumnTaskDto>>> Handle(GetColumnsTasksCommandRequest columnsTasksRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<ColumnTaskDto>> response = new();
            response.IsSuccessful = false;

            List<Domain.Entities.Column> columns = _businessDbContext.Columns.Where(c => c.ProjectId == columnsTasksRequest.ProjectId && c.Status == true).ToList();
            if (columns.Count > 0)
            {
                List<ColumnTaskDto> columnTaskDtos = new();
                foreach (var column in columns)
                {
                    ColumnTaskDto col = new();
                    List<Domain.Entities.Task> tasks = new();
                    TaskDto taskT = new();
                    List<TaskDto> taskDtos= new();

                    col.ProjectId = column.ProjectId;
                    col.Name = column.Name;

                    tasks = _businessDbContext.Tasks.Where(t => t.ProjectId == columnsTasksRequest.ProjectId && t.ColumnId == column.Id && t.Status == true).ToList();
                    foreach(var task in tasks) 
                    {
                        taskT.Id = task.Id;
                        taskT.Name = task.Name;
                        taskT.Priority = task.Priority;
                        taskT.ColumnId= task.ColumnId;

                        taskDtos.Add(taskT);
                    }
                    col.Tasks = taskDtos;

                    columnTaskDtos.Add(col);
                }
                response.Data= columnTaskDtos;
                response.IsSuccessful = true;
            }
            return response;
        }
    }
}
