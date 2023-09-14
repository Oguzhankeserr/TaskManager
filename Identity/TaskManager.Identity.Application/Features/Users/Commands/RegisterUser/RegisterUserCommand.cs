﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
using TaskManager.CommonModels;
using TaskManager.Identity.Application.Features.Users.Commands.RabbitMQ;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.Identity.Domain.Entities;


namespace TaskManager.Identity.Application.Features.Users.Commands.RegisterUser
{
	public class RegisterUserCommandRequest : IRequest<ActionResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }

    public class RegisterUserCommand : IRequestHandler<RegisterUserCommandRequest, ActionResponse<UserDto>>
    {


        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;
		private readonly IRabbitMQService _rabbitMQService;
		private readonly IServiceProvider _serviceProvider; // Inject IServiceProvider here
        

		public RegisterUserCommand(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IRabbitMQService rabbitMQService, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _roleManager = roleManager;
			_rabbitMQService = rabbitMQService;
			_serviceProvider = serviceProvider; // Injected IServiceProvider

		}

		public async Task<ActionResponse<UserDto>> Handle(RegisterUserCommandRequest registerRequest, CancellationToken cancellationToken)
        {
            ActionResponse<UserDto> response = new();
            TaskManager.Identity.Domain.Dtos.UserDto userDto = new();
            response.IsSuccessful = false;

            var roleExists = await _roleManager.RoleExistsAsync(registerRequest.Role);

            var existEmail = await _userManager.FindByEmailAsync(registerRequest.Email);


			

			if (roleExists)
            {
				var baseUserName = registerRequest.UserName;
				var userName = baseUserName;
				var counter = 1;

				if (existEmail == null)

                {
					while (true)
					{
						var existUserName = await _userManager.FindByNameAsync(registerRequest.UserName);

						var theUser = await _userManager.FindByNameAsync(userName);

						if (existUserName == null || theUser == null)
						{
							var user = new AppUser
							{
								Id = Guid.NewGuid().ToString(),
								UserName = userName,
								Name = registerRequest.Name,
								Surname = registerRequest.Surname,
								Email = registerRequest.Email,
								Status = true
							};

							var password = GeneratePassword(3, 3, 3);
							IdentityResult result = await _userManager.CreateAsync(user, password);

							if (result.Succeeded)
							{
								await _userManager.AddToRoleAsync(user, registerRequest.Role);
								userDto.Username = userName;
								userDto.Id = user.Id;
								userDto.Surname = user.Surname;
								userDto.Name = registerRequest.Name;
								userDto.Role = registerRequest.Role;
								userDto.Email = registerRequest.Email;
								userDto.Password = password;
								response.Data = userDto;
								response.IsSuccessful = true;

								await _rabbitMQService.PublishUserRegistrationToRabbitMQ(userDto);
								
								response.Data = userDto;
								counter = 1;
							}
							else
							{
								foreach (var error in result.Errors)
									response.Message += $"{error.Code} - {error.Description}\n";
							}

							break;
						}
						else
						{
							counter++;
							userName = $"{baseUserName}{counter}";

							response.Message = "User created but the username has changed";
						}
					}



				}

				else
				{
					response.Message = "User Email already exist";
				}


				
            }
            else
            {
                response.Message = "Role doesn't exist";
            }
            
            return response;
        }

		public static string GeneratePassword(int lowercase, int uppercase, int numerics)
		{
			string lowers = "abcdefghijklmnopqrstuvwxyz";
			string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string number = "0123456789";

			Random random = new Random();

			string generated = "!";
			for (int i = 1; i <= lowercase; i++)
				generated = generated.Insert(
					random.Next(generated.Length),
					lowers[random.Next(lowers.Length - 1)].ToString()
				);

			for (int i = 1; i <= uppercase; i++)
				generated = generated.Insert(
					random.Next(generated.Length),
					uppers[random.Next(uppers.Length - 1)].ToString()
				);

			for (int i = 1; i <= numerics; i++)
				generated = generated.Insert(
					random.Next(generated.Length),
					number[random.Next(number.Length - 1)].ToString()
				);

			return generated.Replace("!", string.Empty);

		}
	}
	}

