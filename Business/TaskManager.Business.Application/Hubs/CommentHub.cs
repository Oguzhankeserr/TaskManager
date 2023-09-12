using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging; // Add this using statement
using System;
using System.Threading.Tasks;
using TaskManager.Business.Application.Data;

using TaskManager.Business.Application.Models;

namespace TaskManager.Business.Application.Hubs
{
	public class CommentHub : Hub
	{
		static List<string> clients = new List<string>();

		//public async Task SendMessage(Chat.Entities.Chat user)
		//{
		//	await Clients.All.SendAsync("ReceiveMessage", user);
		//}
		public async override Task OnConnectedAsync()
		{
			
			clients.Add(Context.ConnectionId);
		
			await Clients.All.SendAsync("clients", clients);
			await Clients.All.SendAsync("userJoined", $"{Context.ConnectionId}");
		}
		async public override Task OnDisconnectedAsync(Exception? exception)
		{
			clients.Remove(Context.ConnectionId);

			await Clients.All.SendAsync("clients", clients);
			await Clients.All.SendAsync("userLeaved", $"{Context.ConnectionId}");
		}

		public async Task FindUser(string id)
		{
			await Clients.All.SendAsync("findUser", id);
		}

		public async Task AllUsers(string id)
		{
			Client client = new Client
			{
				ConnectionId = Context.ConnectionId,
				id = id
			};
			await Clients.All.SendAsync("allUsers", client);
		}

		public async Task AllUsersLeaved(string id)
		{
			Client client = new Client
			{
				ConnectionId = Context.ConnectionId,
				id = id
			};
			await Clients.All.SendAsync("allUsersLeaved", client);

		}

		//public async Task UserConnected(string userId)
		//{
		//	await Clients.All.SendAsync("UserConnected", userId);
		//}

		//public async Task UserDisconnected(string userId)
		//{
		//	await Clients.All.SendAsync("UserDisconnected", userId);
		//}
		//public async Task GetUserName(string userName)
		//{
		//	Client client = new()
		//	{
		//		ConnectionId = Context.ConnectionId,
		//		id = userName
		//	};

		//	ClientSource.Clients.Add(client);
		//	await Clients.Others.SendAsync("clientJoined", userName);
		//}
	}
}
