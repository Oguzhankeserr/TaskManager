using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Identity.Application.Features.Users.Commands.PasswordChange;
using TaskManager.Identity.Application.Features.Users.Commands.RabbitMQ;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Application.Features.Users.Commands.ForgotPassword
{
	public class ForgotPasswordCommandRequest : IRequest<IActionResult>
	{

		public string email { get; set; }

		public class ForgotPasswordCommand : IRequestHandler<ForgotPasswordCommandRequest, IActionResult>
		{

			readonly UserManager<AppUser> _userManager;
			private readonly IMediator _mediator;
			private readonly ForgotPasswordService _forgotPasswordService;
			private readonly ILogger<ForgotPasswordService> _logger;

			public ForgotPasswordCommand(UserManager<AppUser> userManager, IMediator mediator, ForgotPasswordService forgotPasswordService, ILogger<ForgotPasswordService> logger) // Add dbContext parameter
			{
				_userManager = userManager;
				_mediator = mediator;
				_logger = logger;
				_forgotPasswordService = forgotPasswordService;
			}

			public async Task<IActionResult> Handle(ForgotPasswordCommandRequest request, CancellationToken cancellationToken)
			{
				try
				{
					var email = request.email;
					var user = await _userManager.FindByEmailAsync(email);

					if (user != null)
					{
						await _forgotPasswordService.SendChangePasswordEmail(user.Email, user.Id);
					return new OkResult();

					}
					else
					{
						return new BadRequestResult();
					}

				}

				catch (Exception ex)
				{
					_logger.LogError($"{ex.Message}");
					return new BadRequestResult();


				}







				// Send the password reset email using your email sending logic

			}


		}

		}

	}

