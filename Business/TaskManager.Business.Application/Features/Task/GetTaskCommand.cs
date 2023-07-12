using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.Task
{
    public class GetTaskCommandRequest : IRequest<ActionResponse<List<Domain.Entities.Task>>>
    {
        public int ProjectId { get; set; }
    }

    public class GetTaskCommand : IRequestHandler<GetTaskCommandRequest, ActionResponse<List<Domain.Entities.Task>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetTaskCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<Domain.Entities.Task>>> Handle(GetTaskCommandRequest getTaskCommandRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<Domain.Entities.Task>> response = new();
            response.IsSuccessful = false;
            List<Domain.Entities.Task> task = _businessDbContext.Tasks.Where(p => p.ProjectId == getTaskCommandRequest.ProjectId && p.Status == true).ToList();

            if (task.Count == 0) 
            {
                response.Message = "No columns found in project.";

            }
            response.Data = task;
            response.IsSuccessful = true;
            return response;
            
        }
        
    }
}
