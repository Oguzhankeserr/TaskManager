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

namespace TaskManager.Business.Application.Features.Task
{
    public class GetAllTaskCommandRequest : IRequest<ActionResponse<List<TaskListDto>>>
    {
        public int Id { get; set; } //projectId
    }

    public class GetAllTaskCommand : IRequestHandler<GetAllTaskCommandRequest, ActionResponse<List<TaskListDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetAllTaskCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<TaskListDto>>> Handle(GetAllTaskCommandRequest getAllTaskCommandRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<TaskListDto>> response = new();
            response.IsSuccessful = false;

            List<Domain.Entities.Task> tasks = _businessDbContext.Tasks.Where(p => p.ProjectId == getAllTaskCommandRequest.Id && p.Status == true).ToList();

            if (tasks.Count == 0)
            {
                response.Message = "No tasks found in project.";
                return response;
            }
            else
            {
                List<TaskListDto> taskList = new();
                foreach (var task in tasks)
                {
                    TaskListDto taskListDto = new TaskListDto();
                    taskListDto.Id = task.Id;
                    taskListDto.Name = task.Name;
                    taskListDto.Priority = task.Priority;
                    taskListDto.ColumnId = task.ColumnId;
                    taskListDto.CreatedDate = task.CreatedDate;
                    taskListDto.UpdatedDate = task.UpdatedDate;
                    taskListDto.DueDate = task.EndDate;
                    taskListDto.AssigneeId = task.AssigneeId;
                    taskListDto.ReporterId = task.ReporterId;
                    taskList.Add(taskListDto);
                }

                response.Data = taskList;
            }
            response.IsSuccessful = true;
            return response;

        }

    }
}
