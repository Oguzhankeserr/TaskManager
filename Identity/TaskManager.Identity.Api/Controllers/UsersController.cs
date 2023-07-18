using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.CommonModels;
using TaskManager.Identity.Application.Features.Users.Commands.GetUser;
using TaskManager.Identity.Application.Features.Users.Commands.LoginUser;
using TaskManager.Identity.Application.Features.Users.Commands.RegisterUser;
using TaskManager.Identity.Application.Models;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public  UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

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

        [Authorize(Roles = "x")]
        [HttpGet]
        public async Task<IActionResult> GetUserById([FromBody] GetUserCommandRequest getUserCommandRequest) => Ok(await _mediator.Send(getUserCommandRequest));

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserByIdB([FromBody] GetUserCommandRequest getUserCommandRequest)
        {
            ActionResponse<AppUser> user = await _mediator.Send(getUserCommandRequest);
            return Ok(user);
        }


    } 
}
