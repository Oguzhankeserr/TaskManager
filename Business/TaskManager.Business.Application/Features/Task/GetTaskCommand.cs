﻿using MediatR;
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
    public class GetTaskCommandRequest : IRequest<ActionResponse<List<Domain.Entities.Column>>>
    {
        public int ProjectId { get; set; }
    }

    public class GetTaskCommand : IRequestHandler<GetTaskCommandRequest, ActionResponse<List<Domain.Entities.Column>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetTaskCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<Domain.Entities.Column>>> Handle(GetTaskCommandRequest getTaskCommandRequest, CancellationToken cancellationToken)
        {
            ActionResponse<List<Domain.Entities.Column>> response = new();
            response.IsSuccessful = false;
            List<Domain.Entities.Column> columns = _businessDbContext.Columns.Where(p => p.ProjectId == getTaskCommandRequest.ProjectId && p.Status == true).ToList();

            if (columns.Count == 0) 
            {
                response.Message = "No columns found in project.";

            }
            response.Data = columns;
            response.IsSuccessful = true;
            return response;
            //vdvdgfff
        }
        
    }
}
