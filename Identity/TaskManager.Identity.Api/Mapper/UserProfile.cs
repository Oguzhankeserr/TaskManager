using AutoMapper;
using Microsoft.AspNet.Identity;
using TaskManager.Identity.Application.Models;
using TaskManager.Identity.Domain.Dtos;
using TaskManager.Identity.Domain.Entities;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<AppUser, UserDto>();// Map UserModel to User entity
	}
}
