using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Application.Features.Files;
using TaskManager.Business.Domain.Abstractions.Storage;
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
        //public string TaskId { get; set; }
        //[FromForm(Name = "Files")] //Burası da denedikten sonra silinecek
        //public IFormFile? Files { get; set; }
        public DateTime UserUpdatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AssigneeId { get; set; }
        public string ReporterId { get; set; }

    }

    public class CreateTaskCommand : IRequestHandler<CreateTaskCommandRequest, ActionResponse<Domain.Entities.Task>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly IMediator _mediator;
        readonly IStorageService _storageService;

        public CreateTaskCommand(BusinessDbContext businessDbContext, IMediator mediator, IStorageService storageService)
        {
            _businessDbContext = businessDbContext;
            _mediator = mediator;
            _storageService = storageService;
        }

        public async Task<ActionResponse<Domain.Entities.Task>> Handle(CreateTaskCommandRequest createTaskRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Task> response = new();
            response.IsSuccessful = false;
            Domain.Entities.Task task = new();
            task.Name = createTaskRequest.Name;



            task.ProjectId = createTaskRequest.ProjectId;
            task.ColumnId = createTaskRequest.ColumnId;
            task.Priority = createTaskRequest.Priority;
            task.UserUpdatedDate = createTaskRequest.UserUpdatedDate;
            task.EndDate = createTaskRequest.EndDate;
            task.CreatedDate = task.UpdatedDate = DateTime.UtcNow;
            task.Status = true;
            task.CreatedByUser = task.UpdatedByUser; //admin 
            task.AssigneeId = createTaskRequest.AssigneeId;
            task.ReporterId = createTaskRequest.ReporterId;

            //var fileUpload = _mediator.Send(new UploadTaskFileCommandRequest() { TaskId = Convert.ToInt32(createTaskRequest.TaskId), Files = createTaskRequest.Files }).Result;

            //if (!fileUpload.IsSuccessful)
            //{
            //    response.IsSuccessful = false;
            //    response.Message = fileUpload.Message;
            //    return response;
            //}

            await _businessDbContext.Tasks.AddAsync(task);
            await _businessDbContext.SaveChangesAsync();

            task.CreatedDate = task.UpdatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc); //for return

            response.Data = task;
            response.IsSuccessful = true;

            return response;
        }
    }
}
