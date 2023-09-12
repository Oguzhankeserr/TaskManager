using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Application.Features;
using TaskManager.Business.Application.Models;
using TaskManager.Business.Domain.Entities;
using TaskManager.CommonModels;


namespace TaskManager.Business.Api.Controllers
{
	public class ChatController : ControllerBase

	{
		readonly IMediator _mediator;

		public ChatController(IMediator mediator)
		{
			_mediator = mediator;
		}


		//[HttpPost]
		//public async Task<ActionResponse<Domain.Entities.Chat>> SaveChat(SaveChatCommandRequest saveChatRequest)
		//{
		//	return await _mediator.Send(saveChatRequest);
		//}
	}
}
