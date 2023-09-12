using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManager.CommonModels;
using TaskManager.Identity.Application.Features.Users.Commands.GetUser;
using TaskManager.Identity.Application.Features.Users.Commands.LoginUser;
using TaskManager.Identity.Application.Features.Users.Commands.ForgotPassword;
using TaskManager.Identity.Application.Features.Users.Commands.PasswordChange;
using TaskManager.Identity.Application.Features.Users.Commands.RegisterUser;
using TaskManager.Identity.Application.Models;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.Identity.Domain.Entities;
using TaskManager.Identity.Infrastructure.Context;
using TaskManager.Identity.Application.Features.Users.Commands.SendsEmail;
using Dapper;

namespace TaskManager.Identity.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly TaskManagerDbContext _taskManagerDbContext;
		readonly UserManager<AppUser> _userManager;


		public UsersController(IMediator mediator, TaskManagerDbContext taskManagerDbContext, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
			_userManager = userManager;
			_taskManagerDbContext = taskManagerDbContext;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResponse<UserDto>> RegisterUser(RegisterUserCommandRequest registerUserCommandRequest)
        {
            return await _mediator.Send(registerUserCommandRequest);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserCommandRequest loginUserCommandRequest)
        {
            ActionResponse<TokenDto> token = await _mediator.Send(loginUserCommandRequest);
            return Ok(token);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost] //Get idi Post yaptım
        public async Task<ActionResponse<UserDto>> GetUserById([FromBody] GetUserCommandRequest getUserCommandRequest)
        {
			return await _mediator.Send(getUserCommandRequest);

		}



		[HttpPost]
		public async Task<IActionResult> ChangePasswordWithToken(PasswordChangeCommandRequest passwordChangeCommandRequest)
        {
			return await _mediator.Send(passwordChangeCommandRequest);

		}

		[HttpPost]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordCommandRequest forgotPasswordCommandRequest)
		{
			return await _mediator.Send(forgotPasswordCommandRequest);

		}

        [HttpPost]
        public async Task<IActionResult> SendsEmail(SendsEmailCommandRequest sendsEmailCommandRequest)
        {
            return await _mediator.Send(sendsEmailCommandRequest);

        }

        [HttpGet]
        public async Task<ActionResponse<List<UserDto>>> GetAllUsers()
        {
            ActionResponse<List<UserDto>> response = new();
            response.IsSuccessful = false;

            try
            {
                string userQuery = @"SELECT u.Id, u.Name, u.Surname, u.UserName, u.Email, ur.RoleId AS Role
                    FROM aspnetusers u
                    JOIN  ""AspNetUserRoles"" ur ON u.Id = ur.UserId";


                var users = _taskManagerDbContext.Database.GetDbConnection().QueryAsync<UserDto>(userQuery);
                response.Data = users.Result.ToList();

                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Message = ex.Message;
            }

           
            return response;



        }

		
		

	}
}

