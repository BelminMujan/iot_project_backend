using System;
using IOT_Backend.DTOs.Room;
using IOT_Backend.Models;

namespace IOT_Backend.Services.RoomService
{
	public interface IRoomService
	{
		Task<ServiceResponse<List<GetRoomDto>>> GetAllRooms();

		Task<ServiceResponse<GetRoomDto>> GetRoomById(int id);

		Task<ServiceResponse<List<GetRoomDto>>> AddRoom(AddRoomDto room);

		Task<ServiceResponse<GetRoomDto>> UpdateRoom (UpdateRoomDto room);
		Task<ServiceResponse<List<GetRoomDto>>> DeleteRoom (int id);
	}
}

