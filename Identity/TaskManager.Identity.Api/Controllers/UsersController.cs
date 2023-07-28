﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManager.CommonModels;
using TaskManager.Identity.Application.Features.Users.Commands.GetUser;
using TaskManager.Identity.Application.Features.Users.Commands.LoginUser;
using TaskManager.Identity.Application.Features.Users.Commands.RegisterUser;
using TaskManager.Identity.Application.Models;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.Identity.Domain.Entities;
using TaskManager.Identity.Infrastructure.Context;

namespace TaskManager.Identity.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly TaskManagerDbContext _taskManagerDbContext;

        public UsersController(IMediator mediator, TaskManagerDbContext taskManagerDbContext)
        {
            _mediator = mediator;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUserById([FromBody] GetUserCommandRequest getUserCommandRequest) => Ok(await _mediator.Send(getUserCommandRequest));



        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResponse<List<UserDto>>> GetAllUsers()
        {
            ActionResponse<List<UserDto>> response = new();
            response.IsSuccessful = false;
            List<UserDto> users = new();

            List<AppUser> tableUsers = await _taskManagerDbContext.Users.ToListAsync();

            foreach (AppUser user in tableUsers)
            {
                UserDto userDto = new();
                userDto.Id = user.Id;
                userDto.Name= user.Name;
                userDto.Surname = user.Surname;
                userDto.Username = user.UserName;
                userDto.Email = user.Email;
                users.Add(userDto);
            }

            response.Data = users;
            response.IsSuccessful = true;
            return response;


            
        }
           


    } 
}
