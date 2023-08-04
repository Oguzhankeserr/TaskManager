﻿using System;
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

public class EmailConsumerService : BackgroundService // Background Service : Designed to run in the background and continuously perform its designated tasks.
													  // By running the consumer as a background service, it can continuously listen for new messages without blocking or affecting the main application's responsiveness.
{
	private const string rabbitMQUri = "amqp://guest:guest@localhost:5672";
	private const string queName = "UserRegistrationQueue";

	private readonly IServiceProvider _serviceProvider;
	private readonly ILogger<EmailConsumerService> _logger;

	public EmailConsumerService(IServiceProvider serviceProvider, ILogger<EmailConsumerService> logger)
	{
		 _serviceProvider = serviceProvider;
		_logger = logger;

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
					await SendRegistrationEmail(userDto.Email);

					
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

	private async Task SendRegistrationEmail(string userEmail)
	{
		await Task.Delay(10000); // 10 sec delay to see it on RabbitMQ Management

		string fromMail = "taskmanager0707@gmail.com";
		string fromPassword = "u v j x y q u w u s y w a a z x";

		MailMessage message = new MailMessage();
		message.From = new MailAddress(fromMail);
		message.Subject = "Test Subject";
		message.To.Add(new MailAddress(userEmail));
		message.Body = "<html><body> Test Body </body></html>";
		message.IsBodyHtml = true;

		var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
		{
			Port = 587,
			Credentials = new NetworkCredential(fromMail, fromPassword),
			EnableSsl = true,
		};

		smtpClient.Send(message);

	}


}
