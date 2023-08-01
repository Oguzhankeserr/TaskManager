using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features
{
    public class GetProjectColumnsCommandRequest : IRequest<ActionResponse<List<ColumnDto>>>
    {
        public int Id { get; set; }
    }
    public class GetProjectColumnsCommand : IRequestHandler<GetProjectColumnsCommandRequest, ActionResponse<List<ColumnDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetProjectColumnsCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<ColumnDto>>> Handle(GetProjectColumnsCommandRequest getProjectColumnsRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<ColumnDto>> response = new();
            response.IsSuccessful = false;
            List<Domain.Entities.Column> columns = _businessDbContext.Columns.Where(p=>p.ProjectId == getProjectColumnsRequest.Id && p.Status == true).ToList();
            List<ColumnDto> listDto = new();
            if(columns.Count == 0) 
            {
                response.Message = "No columns found in project.";
            }
            else
            {
                foreach(var column in columns)
                {
                    ColumnDto columnDto = new();
                    columnDto.Id = column.Id;
                    columnDto.Name = column.Name;
                    listDto.Add(columnDto);
                }
                response.Data = listDto;
                response.IsSuccessful = true;
            }
            return response;
        }
        // todo dto
    }
}
