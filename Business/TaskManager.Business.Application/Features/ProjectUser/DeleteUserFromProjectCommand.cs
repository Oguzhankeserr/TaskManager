using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features
{
    public class DeleteUserFromProjectCommandRequest : IRequest<ActionResponse<ProjectUserDto>>
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
    }

    public class DeleteUserFromProjectCommand : IRequestHandler<DeleteUserFromProjectCommandRequest, ActionResponse<ProjectUserDto>>
    {
        readonly BusinessDbContext _businessDbContext;

        public DeleteUserFromProjectCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<ProjectUserDto>> Handle(DeleteUserFromProjectCommandRequest deleteUserRequest, CancellationToken cancellationToken)
        {
            ActionResponse<ProjectUserDto> response = new();
            response.IsSuccessful = false;

            Domain.Entities.ProjectUser projectUser = _businessDbContext.ProjectUsers.FirstOrDefault(p => 
            p.UserId== deleteUserRequest.UserId && p.ProjectId == deleteUserRequest.ProjectId);

            if(projectUser != null)
            {
                projectUser.Status = false;
                await _businessDbContext.SaveChangesAsync();
                response.Message = "User deleted from the project successfully";
                response.IsSuccessful = true;
                return response;
            }
            else
            {
                response.IsSuccessful = false;
                response.Message = "User or project cannot found.";
                return response;    
            }
        }
    }
}
