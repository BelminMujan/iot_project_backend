using System;
using AutoMapper;
using IOT_Backend.DTOs.Room;
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
        }
	}
}

