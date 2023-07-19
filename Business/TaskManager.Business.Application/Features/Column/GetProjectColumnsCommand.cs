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
    public class GetProjectColumnsCommandRequest : IRequest<ActionResponse<List<Domain.Entities.Column>>>
    {
        public int Id { get; set; }
    }
    public class GetProjectColumnsCommand : IRequestHandler<GetProjectColumnsCommandRequest, ActionResponse<List<Domain.Entities.Column>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetProjectColumnsCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<Domain.Entities.Column>>> Handle(GetProjectColumnsCommandRequest getProjectColumnsRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<Domain.Entities.Column>> response = new();
            response.IsSuccessful = false;
            List<Domain.Entities.Column> columns = _businessDbContext.Columns.Where(p=>p.ProjectId == getProjectColumnsRequest.Id && p.Status == true).ToList();
            if(columns.Count == 0) 
            {
                response.Message = "No columns found in project.";
            }
            response.Data = columns;
            response.IsSuccessful = true;
            return response;
            
        }
        // todo dto
    }
}
