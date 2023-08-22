using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Business.Domain;
using TaskManager.Business.Domain.Dtos;
using TaskManager.Business.Domain.Entities;
using TaskManager.Business.Domain.UnitOfWork;
using TaskManager.Business.Infrastructure.Context;
using TaskManager.CommonModels;

namespace TaskManager.Business.Application.Features.ProjectUser
{
    public class AddUserToProjectCommandRequest : IRequest<ActionResponse<ProjectUserDto>>
    {
        public int ProjectId { get; set; }
        public List<ProjectUserList> Users { get; set; }
        
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

            foreach (var user in addUserRequest.Users)
            {
                Domain.Entities.ProjectUser existingUser = _businessDbContext.ProjectUsers.FirstOrDefault(p =>
                    p.ProjectId == addUserRequest.ProjectId && p.UserId == user.Id);

                if (existingUser == null)
                {
                    Domain.Entities.ProjectUser projectUser = new()
                    {
                        ProjectId = addUserRequest.ProjectId,
                        UserId = user.Id,
                        Status = true
                    };

                    await _businessDbContext.ProjectUsers.AddAsync(projectUser);
                }
                else
                {
                    if (existingUser.Status == false)
                    {
                        existingUser.Status = true;
                    }
                    else
                    {
                        response.Message = "User already in the project.";
                    }
                }
            }

            await _businessDbContext.SaveChangesAsync();
            response.IsSuccessful = true;
            response.Message = "Users added to the project successfully.";

            return response;
        }
    }
}
