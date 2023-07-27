using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.ProjectUser
{
    public class AddUserToProjectCommandRequest : IRequest<ActionResponse<ProjectUserDto>>
    {
        public int ProjectId { get; set; }
        public string UserId { get; set; }
    }


    public class AddUserToProjectCommand : IRequestHandler<AddUserToProjectCommandRequest, ActionResponse<ProjectUserDto>>
    {
        readonly BusinessDbContext _businessDbContext;

        public AddUserToProjectCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<ProjectUserDto>> Handle(AddUserToProjectCommandRequest addUserRequest, CancellationToken cancellationToken)
        {
            ActionResponse<ProjectUserDto> response = new();
            response.IsSuccessful = false;

            Domain.Entities.ProjectUser checkUser = _businessDbContext.ProjectUsers.Where(p =>
             p.ProjectId == addUserRequest.ProjectId && p.UserId == addUserRequest.UserId && p.Status == true).FirstOrDefault();

            if (checkUser == null)
            {
                Domain.Entities.ProjectUser projectUser = new()
                {
                    ProjectId = addUserRequest.ProjectId,
                    UserId = addUserRequest.UserId,
                    Status = true
                };

                await _businessDbContext.AddRangeAsync(projectUser);
                await _businessDbContext.SaveChangesAsync();
                response.Message = "User added successfully";
                response.IsSuccessful = true;
                return response;
            }
            else
            {
                response.Message = "User already in the project.";
                response.IsSuccessful = true;
                return response;

            }
        }
    }
}
