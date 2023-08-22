using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features
{
    public class UpdateProjectCommandRequest : IRequest<ActionResponse<Project>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateProjectCommand : IRequestHandler<UpdateProjectCommandRequest, ActionResponse<Project>>
    {
        readonly BusinessDbContext _businessDbContext;

        readonly IRepository<Project> _repository;
        readonly IUnitOfWork<Project> _unitOfWork;
        readonly GenericService<Project> _genericService;

        public UpdateProjectCommand(BusinessDbContext businessDbContext, IRepository<Project> repository, IUnitOfWork<Project> unitOfWork,GenericService<Project> genericService)
        {
            _businessDbContext = businessDbContext;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _genericService = genericService;
        }

        public async Task<ActionResponse<Project>> Handle(UpdateProjectCommandRequest updateProjectRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Project> response = new();
            response.IsSuccessful = false;

            Project project = await _businessDbContext.Projects.FirstOrDefaultAsync(p => p.Id == updateProjectRequest.Id);
            if (project != null && project.Status == true)
            {
                project.Name = updateProjectRequest.Name;
                project.UpdatedDate = DateTime.UtcNow;

                //await _businessDbContext.SaveChangesAsync();
                //_repository.Update(project);
                //_unitOfWork.Commit();
                _genericService.Update(project);

                project.UpdatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc); // for response
                response.Data = project;
                response.IsSuccessful = true;
            }

            return response;
        }
    }
}
