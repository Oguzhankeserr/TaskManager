using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging; // Add this using statement
using System;
using System.Threading.Tasks;

namespace TaskManager.Business.Application.Hubs
{
	public class CommentHub : Hub
	{
	
		public async Task SendMessage(string message)
		{
				await Clients.All.SendAsync("ReceiveMessage", message);
		}
		public async override Task OnConnectedAsync()
		{
			await Clients.All.SendAsync("userJoined", $"{Context.ConnectionId}");
		}

		async public override Task OnDisconnectedAsync(Exception exception)
		{
			await Clients.All.SendAsync("userLeaved", $"{Context.ConnectionId}");
		}
	}
}
