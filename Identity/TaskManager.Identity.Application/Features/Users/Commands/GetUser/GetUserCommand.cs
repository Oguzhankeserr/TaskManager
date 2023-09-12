using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.CommonModels;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Application.Features.Users.Commands.GetUser
{
	public class GetUserCommandRequest : IRequest<ActionResponse<UserDto>>
	{
		public string Id { get; set; }
	}

	public class GetUserCommand : IRequestHandler<GetUserCommandRequest, ActionResponse<UserDto>>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

		public GetUserCommand(UserManager<AppUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}

		public async Task<ActionResponse<UserDto>> Handle(GetUserCommandRequest request, CancellationToken cancellationToken)
		{
			ActionResponse<UserDto> response = new();
			AppUser user = await _userManager.FindByIdAsync(request.Id.ToString());

			response.IsSuccessful = false;
			if (user != null)
			{
				response.IsSuccessful = true;
				response.Data = _mapper.Map<UserDto>(user);
			}
			else
				response.Message = "User Not Found";

			return response;
		}
	}
}
