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
    public class UpdateTaskCommandRequest : IRequest<ActionResponse<Domain.Entities.Task>>
    {
        public int Id { get; set; }
    }

    public class UpdateTaskCommand : IRequestHandler<UpdateTaskCommandRequest, ActionResponse<Domain.Entities.Task>>
    {
        readonly BusinessDbContext _businessDbContext;

        public UpdateTaskCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<Domain.Entities.Task>> Handle(UpdateTaskCommandRequest updateTaskRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Task> response = new();

            return response;
        }
    }
}
