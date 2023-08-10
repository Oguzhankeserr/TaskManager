using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.CommonModels;
using TaskManager.Identity.Application.Models;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Application.Features.Token.Commands
{
	public class PasswordTokenCommandRequest : IRequest<ActionResponse<PasswordToken>>
	{
		public string Id { get; set; }

	}

	public class PasswordTokenCommand : IRequestHandler<PasswordTokenCommandRequest, ActionResponse<PasswordToken>>
	{
		readonly IConfiguration _config;

		public PasswordTokenCommand(IConfiguration config)
		{
			_config = config;
		}

		public async Task<ActionResponse<PasswordToken>> Handle(PasswordTokenCommandRequest request, CancellationToken cancellationToken)
		{
			ActionResponse<PasswordToken> response = new();
			PasswordToken passwordToken = new();

			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_config["Token:SecurityKey"]));
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new List<Claim>
		{
			new Claim("UserId", request.Id) // Adding the user ID claim
        };

			var token = new JwtSecurityToken(
				audience: _config["Token:Audience"],
				issuer: _config["Token:Issuer"],
				expires: DateTime.UtcNow.AddHours(1), // Set token expiration as needed
				signingCredentials: signingCredentials,
				claims: claims
			);

			var tokenHandler = new JwtSecurityTokenHandler();
			passwordToken.PasswordTokenAccess = tokenHandler.WriteToken(token);

			response.Data = passwordToken;
			response.IsSuccessful = true;

			return response;
		}
	}
}
