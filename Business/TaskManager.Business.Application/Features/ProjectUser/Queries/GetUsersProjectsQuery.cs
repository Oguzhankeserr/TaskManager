using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.ProjectUser.Queries
{
    public class GetUsersProjectsRequest : IRequest<ActionResponse<List<ProjectDto>>>
    {
        public string UserId { get; set; }
    }

    public class GetUsersProjectsQuery : IRequestHandler<GetUsersProjectsRequest, ActionResponse<List<ProjectDto>>>
    {
        readonly BusinessDbContext _businessDbContext;

        public GetUsersProjectsQuery(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<ProjectDto>>> Handle(GetUsersProjectsRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<List<ProjectDto>> response = new();
            response.IsSuccessful = false;
            try
            {
                string usersProjectQuery = "Select p.id, p.name, p.description, p.createddate FROM projects p  JOIN projectusers pu ON p.id = pu.projectid WHERE pu.userid = @UserId AND pu.status = true";
                var projects = _businessDbContext.ExecuteQuery<ProjectDto>(usersProjectQuery, new { UserId = request.UserId });
                response.Data = projects.ToList();
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccessful = false;
            }
            return response;
        }
    }

}
