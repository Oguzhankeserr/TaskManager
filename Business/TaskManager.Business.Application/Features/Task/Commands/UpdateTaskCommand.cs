﻿using MediatR;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features
{
    public class UpdateTaskCommandRequest : IRequest<ActionResponse<Domain.Entities.Task>>
    {
        public int Id { get; set; } 
        public string Name{ get; set; }
        public int Priority { get; set; }
        public int? Label { get; set; }
        public string ReporterId { get; set; }
        public string AssigneeId { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }

    public class UpdateTaskCommand : IRequestHandler<UpdateTaskCommandRequest, ActionResponse<Domain.Entities.Task>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly GenericService<Domain.Entities.Task> _genericService;

        public UpdateTaskCommand(BusinessDbContext businessDbContext, GenericService<Domain.Entities.Task> genericService)
        {
            _businessDbContext = businessDbContext;
            _genericService = genericService;
        }

        public async Task<ActionResponse<Domain.Entities.Task>> Handle(UpdateTaskCommandRequest updateTaskRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Task> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Task task = await _businessDbContext.Tasks.FirstOrDefaultAsync(d => d.Id == updateTaskRequest.Id);
            if (task != null && task.Status == true ) 
            {
                task.Name = updateTaskRequest.Name;
                task.Priority = updateTaskRequest.Priority;
                task.ReporterId = updateTaskRequest.ReporterId;
                task.AssigneeId = updateTaskRequest.AssigneeId;
                task.EndDate = updateTaskRequest.EndDate;

                task.UpdatedDate = DateTime.UtcNow;
                task.Label = updateTaskRequest.Label;
                task.Description = updateTaskRequest.Description;

                //await _businessDbContext.SaveChangesAsync();
                _genericService.Update(task);

                response.Data = task;
                response.IsSuccessful = true;
            }

            return response;
        }
    }
}
