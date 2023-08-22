using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features
{
    public class DeleteProjectCommandRequest : IRequest<ActionResponse<Project>>
    {
        public int Id { get; set; }
    }
    public class DeleteProjectCommand : IRequestHandler<DeleteProjectCommandRequest, ActionResponse<Project>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly GenericService<Project> _genericService;

        public DeleteProjectCommand(BusinessDbContext businessDbContext, GenericService<Project> genericService)
        {
            _businessDbContext = businessDbContext;
            _genericService = genericService;
        }

        public async Task<ActionResponse<Project>> Handle(DeleteProjectCommandRequest deleteProjectRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Project> response = new();
            response.IsSuccessful = false;

            Project project = await _businessDbContext.Projects.FirstOrDefaultAsync(p => p.Id == deleteProjectRequest.Id);
            if(project != null && project.Status == true )
            {
                project.Status = false;
                //await _businessDbContext.SaveChangesAsync();
                _genericService.Update(project);
                response.Data = project;
                response.IsSuccessful = true;
                //todo update time 
            }
            return response;
        }
    }
}
