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
    public class UpdateTaskColumnIdCommandRequest : IRequest<ActionResponse<Domain.Entities.Task>>
    {
        public int Id { get; set; }
        public int ColumnId { get; set; }
    }
    
    public class UpdateTaskColumnIdCommand : IRequestHandler<UpdateTaskColumnIdCommandRequest, ActionResponse<Domain.Entities.Task>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly GenericService<Domain.Entities.Task> _genericService;

        public UpdateTaskColumnIdCommand(BusinessDbContext businessDbContext, GenericService<Domain.Entities.Task> genericService)
        {
            _businessDbContext = businessDbContext;
            _genericService = genericService;
        }

        public async Task<ActionResponse<Domain.Entities.Task>> Handle(UpdateTaskColumnIdCommandRequest updateTaskColumnIdCommandRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Task> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Task task = await _businessDbContext.Tasks.FirstOrDefaultAsync(p => p.Id == updateTaskColumnIdCommandRequest.Id);
            if (task != null && task.Status ==true) 
            {
                task.ColumnId = updateTaskColumnIdCommandRequest.ColumnId;

                task.UpdatedDate = DateTime.UtcNow;

                //await _businessDbContext.SaveChangesAsync();
                _genericService.Update(task);

                response.Data = task;
                response.IsSuccessful = true;
            }
            return response;
        }
    }
}
