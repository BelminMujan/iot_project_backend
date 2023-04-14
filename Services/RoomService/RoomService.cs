using System;
using System.Security.Claims;
using AutoMapper;
using IOT_Backend.Data;
using IOT_Backend.DTOs.Room;
using IOT_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace IOT_Backend.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly DataContext context;

        public RoomService(IMapper mapper, DataContext context)
        {
            this.context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetRoomDto>>> AddRoom(AddRoomDto room)
        {
            var sr = new ServiceResponse<List<GetRoomDto>>();
            var newRoom = _mapper.Map<Room>(room);
            newRoom.Id = context.Room.Any() ? Convert.ToInt32(context.Room.Max(s => s.Id)) + 1 : 1;
            context.Room.Add(newRoom);
            newRoom.UserId = 2;
            context.SaveChanges();
            sr.Data = await context.Room.Select(s => _mapper.Map<GetRoomDto>(s)).ToListAsync();
            return sr;
        }

        public async Task<ServiceResponse<List<GetRoomDto>>> DeleteRoom(int id)
        {
            var sr = new ServiceResponse<List<GetRoomDto>>();
            try
            {
                var rr = context.Room.First(s => s.Id == id);
                if (rr is null)
                {
                    throw new Exception($"Room with Id '{id}' not found!");
                }
                context.Room.Remove(rr);
                context.SaveChanges();
                sr.Data = await context.Room.Select(s => _mapper.Map<GetRoomDto>(s)).ToListAsync();
            }
            catch (Exception ex)
            {
                sr.Success = false;
                sr.Message = ex.Message;

            }
            return sr;
        }

        public async Task<ServiceResponse<List<GetRoomDto>>> GetAllRooms(int uid)
        {
            var sr = new ServiceResponse<List<GetRoomDto>>();
            sr.Data = await context.Room.Where(r=>r.UserId == uid).Select(s => _mapper.Map<GetRoomDto>(s)).ToListAsync();
            return sr;
        }

        public async Task<ServiceResponse<GetRoomDto>> GetRoomById(int id)
        {
            var sr = new ServiceResponse<GetRoomDto>();
            var ss = await context.Room.FirstOrDefaultAsync(r => r.Id == id);
            sr.Data = _mapper.Map<GetRoomDto>(ss);
            return sr;
        }

        public async Task<ServiceResponse<GetRoomDto>> UpdateRoom(UpdateRoomDto room)
        {
            var sr = new ServiceResponse<GetRoomDto>();
            try
            {
                Console.WriteLine("updating room");
                var rr = await context.Room.FirstOrDefaultAsync(s => s.Id == room.Id);
                if (rr is null)
                {
                    throw new Exception($"Room with Id '{room.Id}' not found!");
                }
                rr.xAxis = room.xAxis;
                rr.yAxis = room.yAxis;
                rr.Title = room.Title;
                rr.HasSensor = room.HasSensor;
                context.SaveChanges();
                sr.Data = _mapper.Map<GetRoomDto>(rr);
            }
            catch (Exception ex)
            {
                sr.Success = false;
                sr.Message = ex.Message;

            }

            return sr;
        }
    }
}

