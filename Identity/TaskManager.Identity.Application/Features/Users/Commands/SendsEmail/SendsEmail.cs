using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.Identity.Application.Features.Users.Commands.RabbitMQ;
using TaskManager.Identity.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Identity.Application.Features.Users.Commands.SendsEmail
{
    public class SendsEmailCommandRequest : IRequest<IActionResult>
    {

        public string Message { get; set; }
        public UserDto[] Users { get; set; }

        public class SendsEmailCommand : IRequestHandler<SendsEmailCommandRequest, IActionResult>
        {

            readonly UserManager<AppUser> _userManager;
            private readonly IMediator _mediator;
            private readonly SendsEmailService _sendsEmailService;
            private readonly ILogger<SendsEmailService> _logger;

            public SendsEmailCommand(UserManager<AppUser> userManager, IMediator mediator, SendsEmailService sendsEmailService, ILogger<SendsEmailService> logger) // Add dbContext parameter
            {
                _userManager = userManager;
                _mediator = mediator;
                _logger = logger;
                _sendsEmailService = sendsEmailService;
            }

            public async Task<IActionResult> Handle(SendsEmailCommandRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var message = request.Message;
                    var users = request.Users;

                    if (users != null)
                    {

                        await _sendsEmailService.SendEmailToUsers(message, users); 
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
