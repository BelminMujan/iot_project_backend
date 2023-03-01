using System;
using AutoMapper;
using IOT_Backend.DTOs.Room;
using IOT_Backend.DTOs.User;
using IOT_Backend.Models;

namespace IOT_Backend
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Room, GetRoomDto>();
			CreateMap<Room, AddRoomDto>();
			CreateMap<AddRoomDto, Room>();
			CreateMap<RegisterUserDto, User>();
			CreateMap<User, RegisterUserDto>();
			CreateMap<LoginUserDto, User>();
			CreateMap<User, LoginUserDto>();
			CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();
        }
	}
}

