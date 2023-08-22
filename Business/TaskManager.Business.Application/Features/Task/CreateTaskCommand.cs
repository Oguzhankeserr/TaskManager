using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Application.Features.Files;
using TaskManager.Business.Domain;
using TaskManager.Business.Domain.Abstractions.Storage;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using TaskManager.CommonModels.Repositories;

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
        //public DateTime UserUpdatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AssigneeId { get; set; }
        public string ReporterId { get; set; }

    }

    public class CreateTaskCommand : IRequestHandler<CreateTaskCommandRequest, ActionResponse<Domain.Entities.Task>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly IMediator _mediator;
        readonly IStorageService _storageService;
        readonly IUserInfoRepository _userInfoRepository;
        readonly GenericService<Domain.Entities.Task> _genericService;
        readonly IRepository<Domain.Entities.Task> _repository;
        readonly IUnitOfWork<Domain.Entities.Task> _uow;
        public CreateTaskCommand(BusinessDbContext businessDbContext, IMediator mediator, IStorageService storageService, IUserInfoRepository userInfoRepository, GenericService<Domain.Entities.Task> genericService, IRepository<Domain.Entities.Task> repository, IUnitOfWork<Domain.Entities.Task> uow)
        {
            _businessDbContext = businessDbContext;
            _mediator = mediator;
            _storageService = storageService;
            _userInfoRepository = userInfoRepository;
            _genericService = genericService;
            _repository = repository;
            _uow = uow;
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
            task.UserUpdatedDate = DateTime.UtcNow;
			task.EndDate = createTaskRequest.EndDate;
            task.CreatedDate = task.UpdatedDate = DateTime.UtcNow;
            task.Status = true;
            task.CreatedByUser = _userInfoRepository.User.UserId;
            task.AssigneeId = createTaskRequest.AssigneeId;
            task.ReporterId = createTaskRequest.ReporterId;


            //await _businessDbContext.Tasks.AddAsync(task);
            //await _businessDbContext.SaveChangesAsync();
            _genericService.Add(task);


            task.CreatedDate = task.UpdatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc); //for return

            response.Data = task;
            response.IsSuccessful = true;

            return response;
        }

    }
}
