using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.Identity.Domain.Entities;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaskManager.Identity.Application.Features.Token.Commands;
using MediatR;
using System.Xml.Linq;

public class EmailConsumerService : BackgroundService // Background Service : Designed to run in the background and continuously perform its designated tasks.
													  // By running the consumer as a background service, it can continuously listen for new messages without blocking or affecting the main application's responsiveness.
{
	private const string rabbitMQUri = "amqp://guest:guest@localhost:5672";
	private const string queName = "UserRegistrationQueue";

	private readonly IServiceProvider _serviceProvider;
	private readonly ILogger<EmailConsumerService> _logger;
	readonly IMediator _mediator;


	public EmailConsumerService(IServiceProvider serviceProvider, ILogger<EmailConsumerService> logger, IMediator mediator)
	{
		 _serviceProvider = serviceProvider;
		_logger = logger;
		_mediator = mediator;

	}
	//z
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var factory = new ConnectionFactory() { Uri = new Uri(rabbitMQUri) };

		using (var connection = factory.CreateConnection())
		using (var channel = connection.CreateModel())
		{
			channel.QueueDeclare(queName, false, false, false, null);

			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += async (sender, e) =>
			{
				var body = e.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				var userDto = System.Text.Json.JsonSerializer.Deserialize<UserDto>(message);

				try
				{
					await SendRegistrationEmail(userDto.Email, userDto.Username, userDto.Name, userDto.Password, userDto.Id);

					
					_logger.LogInformation($"Email sent successfully to {userDto.Email}");
				}
				catch (Exception ex)
				{
					_logger.LogError($"Error sending email to {userDto.Email}: {ex.Message}");
					
				}

				channel.BasicAck(e.DeliveryTag, false);
			};

			channel.BasicConsume(queName, false, consumer);

			await Task.Delay(Timeout.Infinite, stoppingToken); // It is used to keep the background service running indefinitely until it is explicitly stopped or the application is shut down.
		}
	}

	public async Task SendRegistrationEmail(string userEmail,string username, string name, string password, string Id)
	{
		//await Task.Delay(10000); // 10 sec delay to see it on RabbitMQ Management

		string fromMail = "taskmanager0707@gmail.com";
		string fromPassword = "u v j x y q u w u s y w a a z x";
		
		
		string token = await GeneratePasswordResetToken(Id); // Call a method to generate the token
		string resetUrl = $"http://localhost:4200/password-change?token={token}";

		string emailBody = $"<html><body> Hello {name} <br> This is your current username and password : <br> Username : {username} <br> {password}<br> And this is your <a href='{resetUrl}'> determine password </a>  link. </body></html>";

		MailMessage message = new MailMessage();
		message.From = new MailAddress(fromMail);
		message.Subject = "Reset Password";
		message.To.Add(new MailAddress(userEmail));
		message.Body = emailBody;
		message.IsBodyHtml = true;

		var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
		{
			Port = 587,
			Credentials = new NetworkCredential(fromMail, fromPassword),
			EnableSsl = true, 
		};

		smtpClient.Send(message);

	}

	public async Task<string> GeneratePasswordResetToken(string userId)
	{

		var command = new PasswordTokenCommandRequest { Id = userId };
		var result = await _mediator.Send(command); // Inject and use MediatR to send the command

		return result.Data.PasswordTokenAccess;
	}

	


}
