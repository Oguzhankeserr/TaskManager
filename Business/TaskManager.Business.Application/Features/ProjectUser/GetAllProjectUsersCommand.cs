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
    public class GetAllProjectUsersCommandRequest : IRequest<ActionResponse<List<UserDto>>>
    {
        public int Id { get; set; }
    }

    public class GetAllProjectUsersCommand : IRequestHandler<GetAllProjectUsersCommandRequest, ActionResponse<List<UserDto>>> 
    {
        readonly BusinessDbContext _businessDbContext;

        public GetAllProjectUsersCommand(BusinessDbContext businessDbContext)
        {
            _businessDbContext = businessDbContext;
        }

        public async Task<ActionResponse<List<UserDto>>> Handle(GetAllProjectUsersCommandRequest getAllRequest,  CancellationToken cancellationToken)
        {
            ActionResponse<List<UserDto>> response = new();
            response.IsSuccessful = false;

            List<Domain.Entities.ProjectUser> users = _businessDbContext.ProjectUsers.Where(p => p.ProjectId == getAllRequest.Id && p.Status == true).ToList();
            if(users != null)
            {
                List<UserDto> userList = new();
                foreach (var user in users)
                {
                    UserDto userDto = new();
                    userDto.Id = user.UserId;
                    userList.Add(userDto);

                }
                response.Data = userList;
                response.IsSuccessful = true;
                return response;
            }
            else
            {
                response.Message = "No users in the project";
                return response;
            }
            


        }
    }
}
