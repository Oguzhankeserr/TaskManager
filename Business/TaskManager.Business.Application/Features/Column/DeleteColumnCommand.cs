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
    public class DeleteColumnCommandRequest : IRequest<ActionResponse<Domain.Entities.Column>>
    {
        public int Id { get; set; }
    }

    public class DeleteColumnCommand : IRequestHandler<DeleteColumnCommandRequest, ActionResponse<Domain.Entities.Column>>
    {
        readonly BusinessDbContext _businessDbContext;

        public DeleteColumnCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<Domain.Entities.Column>> Handle(DeleteColumnCommandRequest deleteColumnRequest, CancellationToken cancellationToken)
        {
            ActionResponse<Domain.Entities.Column> response = new();
            response.IsSuccessful = false;

            Domain.Entities.Column column = await _businessDbContext.Columns.FirstOrDefaultAsync(c => c.Id == deleteColumnRequest.Id);
            if(column != null && column.Status == true)
            {
                column.Status = false;
                await _businessDbContext.SaveChangesAsync();
                response.Data = column;
                response.IsSuccessful |= true;
            }
            return response;
        }
    }
}
