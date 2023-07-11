using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features
{
    public class CreateTaskCommandRequest : IRequest<ActionResponse<Domain.Entities.Task>>
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int ColumnId { get; set; }
        public int Priority { get; set; }
        public DateTime UserUpdatedDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CreateTaskCommand : IRequestHandler<CreateTaskCommandRequest, ActionResponse<Domain.Entities.Task>>
    {
        readonly BusinessDbContext _businessDbContext;

        public CreateTaskCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<Domain.Entities.Task>> Handle(CreateTaskCommandRequest createTaskRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Task> response = new();
            response.IsSuccessful = false;
            Domain.Entities.Task task = new();
            task.Name = createTaskRequest.Name;
            task.ProjectId = createTaskRequest.ProjectId;
            task.ColumnId= createTaskRequest.ColumnId;
            task.Priority = createTaskRequest.Priority;
            task.UserUpdatedDate = createTaskRequest.UserUpdatedDate;
            task.EndDate = createTaskRequest.EndDate;
            task.CreatedDate = task.UpdatedDate = DateTime.UtcNow;
           // task.CreatedByUser = task.UpdatedByUser //admin 

            await _businessDbContext.Tasks.AddAsync(task);
            await _businessDbContext.SaveChangesAsync();

            task.CreatedDate = task.UpdatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc); //for return

            response.Data = task;
            response.IsSuccessful = true;

            return response;
        }
    }
}
