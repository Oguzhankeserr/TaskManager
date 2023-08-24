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


namespace TaskManager.Business.Application.Features.Task
{
    public class GetAllTaskForUserCommandRequest : IRequest<ActionResponse<List<TaskUserDto>>>
    {
        // get userId
        public string Id { get; set; } 
    }

        public class GetAllTaskForUserCommand : IRequestHandler<GetAllTaskForUserCommandRequest, ActionResponse<List<TaskUserDto>>>
        {
        readonly BusinessDbContext _businessDbContext;

        public GetAllTaskForUserCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        async public Task<ActionResponse<List<TaskUserDto>>> Handle(GetAllTaskForUserCommandRequest getAllTaskForUserCommandRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<TaskUserDto>> response = new();
            response.IsSuccessful = false;

            List<Domain.Entities.ProjectUser> users =  _businessDbContext.ProjectUsers.Where(person => person.UserId == getAllTaskForUserCommandRequest.Id).ToList();

            List<Domain.Entities.Task> tasks = new();
            foreach (var user in users )
            {
                
                tasks = _businessDbContext.Tasks.Where(t => t.AssigneeId == user.UserId && t.Status).ToList();
            }


            if (tasks.Count == 0)
            {
                response.Message = "No task found";
                return response;
            }
            else
            {
                List<TaskUserDto> taskUser = new List<TaskUserDto>();
                foreach (var task in tasks)
                {
                    TaskUserDto taskUserDto = new TaskUserDto();
                    taskUserDto.Id = task.Id;
                    taskUserDto.Name = task.Name;
                    taskUserDto.ProjectId = task.ProjectId;
                    taskUserDto.Priority = task.Priority;
                    taskUserDto.AssigneeId = task.AssigneeId;
                    taskUserDto.Label = task.Label;

                    taskUser.Add(taskUserDto);
                }
                response.Data = taskUser;
            }
            response.IsSuccessful = true;
            return response;
        }
    }
}
