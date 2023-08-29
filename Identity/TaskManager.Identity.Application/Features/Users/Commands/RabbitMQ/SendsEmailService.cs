﻿using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity.Domain.Dtos;
using System.Net.Mail;
using System.Net;
using System.Xml.Linq;
using Microsoft.Extensions.Hosting;

namespace TaskManager.Identity.Application.Features.Users.Commands.RabbitMQ
{
    public class SendsEmailService : BackgroundService
    {

        private const string rabbitMQUri = "amqp://guest:guest@localhost:5672";
        private const string queName = "UserRegistrationQueue";

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EmailConsumerService> _logger;
        readonly IMediator _mediator;

        public SendsEmailService(IServiceProvider serviceProvider, ILogger<EmailConsumerService> logger, IMediator mediator)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _mediator = mediator;

        }
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
                    var username = userDto.Username;
                    var name = userDto.Name;
                    var password = userDto.Password;
                    var Id = userDto.Id;

                    try
                    {
                        var emailList = new List<string> { "email1@example.com", "email2@example.com", "email3@example.com" };
                        await SendRegistrationEmail(emailList, username, name, password, Id);



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
        public async Task SendRegistrationEmail(List<string> userEmails, string username, string name, string password, string Id)
        {
            //await Task.Delay(10000); // 10 sec delay to see it on RabbitMQ Management

            string fromMail = "taskmanager0707@gmail.com";
            string fromPassword = "u v j x y q u w u s y w a a z x";



            string emailBody = $"<html><body> Hello {name} <br> This is your current username and password : <br> Username : {username} <br> {password}<br> And this is your determine password </a>  link. </body></html>";

            foreach (var userEmail in userEmails)
            {
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
        }


    }
}
