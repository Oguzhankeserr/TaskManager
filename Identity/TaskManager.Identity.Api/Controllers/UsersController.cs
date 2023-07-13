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
using TaskManager.Identity.Domain.Entities;

namespace TaskManager.Identity.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private IMediator _mediator;

        public  UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserCommandRequest registerUserCommandRequest)
        {
            RegisterUserCommandResponse response = await _mediator.Send(registerUserCommandRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserCommandRequest loginUserCommandRequest)
        {
            ActionResponse<TokenDto> token = await _mediator.Send(loginUserCommandRequest);
            return Ok(token);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync([FromQuery] GetUserCommandRequest getUserCommandRequest) => Ok(await _mediator.Send(getUserCommandRequest));
       
    } 
}
