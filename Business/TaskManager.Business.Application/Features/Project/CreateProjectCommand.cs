using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Application.Features.ProjectUser;
using TaskManager.Business.Domain;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using TaskManager.CommonModels.Repositories;


namespace TaskManager.Business.Application.Features
{
    public class CreateProjectCommandRequest : IRequest<ActionResponse<Project>>
    {
        public string Name { get; set; }
    }

    public class CreateProjectCommand : IRequestHandler<CreateProjectCommandRequest, ActionResponse<Project>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly IRepository<Project> _repository;
        readonly IUnitOfWork _uow;
        readonly IUserInfoRepository _userInfoRepository;
        readonly IMediator _mediator;

        public CreateProjectCommand(BusinessDbContext businessDbContext, IRepository<Project> repository, IUnitOfWork uow, IUserInfoRepository userInfoRepository, IMediator mediator)
        {
            _businessDbContext = businessDbContext;
            _repository = repository;
            _uow = uow;
            _userInfoRepository = userInfoRepository;
            _mediator = mediator;
        }

        public async Task<ActionResponse<Project>> Handle(CreateProjectCommandRequest createProjectRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Project> response = new();
            response.IsSuccessful = false;

            Project project = new();
            
            project.Name = createProjectRequest.Name;
            project.Status = true;
            project.CreatedDate = project.UpdatedDate = DateTime.UtcNow; //database get local time but response takes utc (wrong time)

            //todo get token id 
            project.CreatedByUser = _userInfoRepository.User.UserId;

            //await _businessDbContext.Projects.AddAsync(project);
            //await _businessDbContext.SaveChangesAsync();

            await _repository.AddAsync(project);
            await _uow.CommitAsync();

            var addUserRequest = new AddUserToProjectCommandRequest
            {
                ProjectId = project.Id,
                Users = new List<AddUserToProject>
            {
                new AddUserToProject
                {
                    UserId = _userInfoRepository.User.UserId.ToString(),
                    Username = _userInfoRepository.User.Username
                }
            }
            };

            var addUserResponse = await _mediator.Send(addUserRequest);

            if (addUserResponse.IsSuccessful)
            {
                response.IsSuccessful = true;
                response.Message = "User and project added successfully";
            }

            //project.CreatedDate = project.UpdatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc); // for response gets local time

            response.Data = project;
            response.IsSuccessful = true;
            return response;

        }
    }
}
