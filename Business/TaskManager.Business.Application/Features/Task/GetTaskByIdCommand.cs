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
    public class GetTaskByIdCommandRequest : IRequest<ActionResponse<Domain.Entities.Task>>
    {
        public int Id { get; set; }
    }
    public class GetTaskByICommand : IRequestHandler<GetTaskByIdCommandRequest, ActionResponse<Domain.Entities.Task>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetTaskByICommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }
        public async Task<ActionResponse<Domain.Entities.Task>>Handle(GetTaskByIdCommandRequest getTaskByIdRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Task> response = new();
            response.IsSuccessful = false;
            Domain.Entities.Task task = await _businessDbContext.Tasks.FirstOrDefaultAsync(p=>p.Id == getTaskByIdRequest.Id);
            if(task != null && task.Status == true) 
            {
                response.Data = task;
                response.IsSuccessful = true;
            }
            else
            {
                response.Message = "No such task found";
            }
            return response;
        }

    }
}
