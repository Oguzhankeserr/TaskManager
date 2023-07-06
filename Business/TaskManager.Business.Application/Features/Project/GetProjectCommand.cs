using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features
{
    public class GetProjectCommandRequest : IRequest<ActionResponse<Project>>
    {
        public int Id { get; set; }
    }
    public class GetProjectCommand : IRequestHandler<GetProjectCommandRequest, ActionResponse<Project>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetProjectCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }
    
        public async Task<ActionResponse<Project>> Handle(GetProjectCommandRequest getProjectRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Project> response = new();
            response.IsSuccessful = false;

            Project project  = await _businessDbContext.Projects.FirstOrDefaultAsync(p => p.Id == getProjectRequest.Id);
            if(project != null && project.Status == true)
            {
                response.Data = project;
                response.IsSuccessful = true;
            }
            else
            {
                response.Message = "No such project found.";
            }
            return response;
        }
    }
}
