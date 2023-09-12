using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;
using TaskManager.CommonModels.Repositories;

namespace TaskManager.Business.Application.Features.ProjectUser.Commands
{
    public class DeleteUserFromProjectRequest : IRequest<ActionResponse<ProjectUserDto>>
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
    }

    public class DeleteUserFromProjectCommand : IRequestHandler<DeleteUserFromProjectRequest, ActionResponse<ProjectUserDto>>
    {
        readonly BusinessDbContext _businessDbContext;
        readonly GenericService<Domain.Entities.ProjectUser> _projectUserGenericService;

        public DeleteUserFromProjectCommand(BusinessDbContext businessDbContext, GenericService<Domain.Entities.ProjectUser> projectUserGenericService)
        {
            _businessDbContext = businessDbContext;
            _projectUserGenericService = projectUserGenericService;
        }

        public async Task<ActionResponse<ProjectUserDto>> Handle(DeleteUserFromProjectRequest request, CancellationToken cancellationToken)
        {
            ActionResponse<ProjectUserDto> response = new();
            response.IsSuccessful = false;
            try
            {
                var user = _businessDbContext.ProjectUsers.SingleOrDefault(user => user.ProjectId == request.ProjectId && user.UserId == request.UserId && user.Status == true);

                if (user != null)
                {
                    user.Status = false;
                    _projectUserGenericService.Update(user);
                    response.IsSuccessful = true;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Message = "User cannot found";

                }
            }
            catch (Exception ex)
            {
                response.Message += ex.ToString();
                response.IsSuccessful = false;
            }


            return response;
        }
    }

}
