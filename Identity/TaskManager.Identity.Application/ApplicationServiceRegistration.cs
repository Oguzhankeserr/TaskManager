 using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity.Application.Features.Users.Commands.RabbitMQ;
using Microsoft.Extensions.Hosting;
using Org.BouncyCastle.Tsp;
using Org.BouncyCastle.Utilities;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using System.Threading.Channels;

namespace TaskManager.Identity.Application
{
    public static class ApplicationServiceRegistration
    {
		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
			services.AddTransient<IRabbitMQService, RabbitMQService>();
			services.AddHostedService<EmailConsumerService>(); // AddHostedService :  It is used to register a background service 
			services.AddScoped<ForgotPasswordService>();// AddHosted Yapınca çalışmıyor. AddScoped doğrusu mu ?
		}


	}
}
