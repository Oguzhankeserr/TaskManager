using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.CommonModels;
using TaskManager.Identity.Application.Features.Token.Commands;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Application.Features.Users.Commands.PasswordChange
{
	public class PasswordChangeCommandRequest : IRequest<IActionResult>
	{
		public string Token { get; set; }
		public string NewPassword {get; set;}

		public class PasswordChangeCommand : IRequestHandler<PasswordChangeCommandRequest, IActionResult>
		{
			readonly UserManager<AppUser> _userManager;


			public PasswordChangeCommand(UserManager<AppUser> userManager)
			{
				_userManager = userManager;

			}

			public async Task<IActionResult> Handle(PasswordChangeCommandRequest request, CancellationToken cancellationToken) // True / False mu dönmeliyim ?
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var token = tokenHandler.ReadJwtToken(request.Token);
				var newPassword = request.NewPassword;

				var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == "UserId");
				if (userIdClaim != null)
				{
					var userId = userIdClaim.Value;
					var user = await _userManager.FindByIdAsync(userId);

					if (user != null)
					{
						var result = await _userManager.RemovePasswordAsync(user); // Remove the existing password
						if (result.Succeeded)
						{
							result = await _userManager.AddPasswordAsync(user, request.NewPassword); // Add the new password

							return new OkResult();

						}
						else
						{
							return new BadRequestResult();
						}

					}
					return new OkResult();

				}
				else
				{

					return new BadRequestResult();
				}

			}
		}
	}
}
