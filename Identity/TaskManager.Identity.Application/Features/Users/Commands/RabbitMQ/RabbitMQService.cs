using RabbitMQ.Client;
using System.Text;
using TaskManager.Identity.Domain.Dtos;

namespace TaskManager.Identity.Application.Features.Users.Commands.RabbitMQ
{
	public interface IRabbitMQService
	{
		Task PublishUserRegistrationToRabbitMQ(UserDto userDto);
	}

	public class RabbitMQService : IRabbitMQService
	{
		public async Task PublishUserRegistrationToRabbitMQ(UserDto userDto)
		{
			// RabbitMQ configuration
			string rabbitMQUri = "amqp://guest:guest@localhost:5672";
			string exchangeName = "UserRegistrationExchange";
			string routingKey = "user-registration";
			string queName = "UserRegistrationQueue";

			var factory = new ConnectionFactory() { Uri = new Uri(rabbitMQUri) };

			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
				channel.QueueDeclare(queName, false, false, false, null);
				channel.QueueBind(queName, exchangeName, routingKey);

				// Create a JSON string representing the user registration details
				var userRegistrationDetails = System.Text.Json.JsonSerializer.Serialize(userDto);
				var messageBodyBytes = Encoding.UTF8.GetBytes(userRegistrationDetails);

				channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
			}
		}
	}
}
