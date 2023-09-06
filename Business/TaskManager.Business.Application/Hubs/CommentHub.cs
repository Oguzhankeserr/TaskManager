using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging; // Add this using statement
using System;
using System.Threading.Tasks;

namespace TaskManager.Business.Application.Hubs
{
	public class CommentHub : Hub
	{
		private readonly ILogger<CommentHub> _logger;

		public CommentHub(ILogger<CommentHub> logger)
		{
			_logger = logger;
		}

		public async Task SendMessage(string message)
		{
			try
			{
				// Log when the method starts
				_logger.LogInformation("SendMessage method called with message: {Message}", message);

				// Your hub logic here...

				// Log when the method completes successfully
				_logger.LogInformation("SendMessage method completed successfully");

				// Send the message to all clients
				await Clients.All.SendAsync("ReceiveMessage", message);
			}
			catch (Exception ex)
			{
				// Log any exceptions that occur
				_logger.LogError(ex, "Error in SendMessage method");
			}
		}
	}
}
