using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.Task
{
    public class DeleteTaskCommandRequest : IRequest<ActionResponse<Domain.Entities.Task>>
    {
        public int Id { get; set; }
    }
    public class DeleteTaskCommand : IRequestHandler<DeleteTaskCommandRequest, ActionResponse<Domain.Entities.Task>>
    {
        readonly BusinessDbContext _businessDbContext;

        public DeleteTaskCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

    public async Task<ActionResponse<Domain.Entities.Task>> Handle(DeleteTaskCommandRequest deleteTaskRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Task> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Task task = await _businessDbContext.Tasks.FirstOrDefaultAsync(c => c.Id == deleteTaskRequest.Id);
            if (task != null && task.Status == true) 
            {
                task.Status = false;
                await _businessDbContext.SaveChangesAsync();
                response.Data=task;
                response.IsSuccessful = true;
            }
            return response;
        }
    }
}
