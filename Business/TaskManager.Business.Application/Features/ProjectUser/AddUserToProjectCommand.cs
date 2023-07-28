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

namespace TaskManager.Business.Application.Features.ProjectUser
{
    public class AddUserToProjectCommandRequest : IRequest<ActionResponse<ProjectUserDto>>
    {
        public int ProjectId { get; set; }
        public List<AddUserToProject> Users { get; set; }
        
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

            List<Domain.Entities.ProjectUser> addedUsers = new();

            foreach (var user in addUserRequest.Users)
            {
                Domain.Entities.ProjectUser checkUser = _businessDbContext.ProjectUsers.Where(p =>
                    p.ProjectId == addUserRequest.ProjectId && p.UserId == user.UserId && p.Status == true).FirstOrDefault();

                if (checkUser == null)
                {
                    Domain.Entities.ProjectUser projectUser = new()
                    {
                        ProjectId = addUserRequest.ProjectId,
                        UserId = user.UserId,
                        Username = user.Username,
                        Status = true
                    };

                    await _businessDbContext.ProjectUsers.AddAsync(projectUser);
                    addedUsers.Add(projectUser);
                }
                else
                {
                    response.Message = "User already in the project.";
                }
            }
            await _businessDbContext.SaveChangesAsync();
            return response;
           
        }
    }
}
