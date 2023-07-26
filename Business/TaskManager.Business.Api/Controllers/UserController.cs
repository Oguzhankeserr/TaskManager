using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Business.Domain.Entities;
using TaskManager.CommonModels;

namespace TaskManager.Business.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        //[HttpPost]
        //public async Task<ActionResponse<UserInfo>> CreateUserInfo(CreateUserInfoCommandRequest userInfoCommandRequest)
        //{
        //    return await _mediator.Send(userInfoCommandRequest);
        //}

    }
}
