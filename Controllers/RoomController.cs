using System;
using System.Security.Claims;
using IOT_Backend.DTOs.Room;
using IOT_Backend.Models;
using IOT_Backend.Services.RoomService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace IOT_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IHubContext<TemperatureHub> hubContext;

        public RoomController(IRoomService roomService, IHubContext<TemperatureHub> hubContext)
        {
            this.hubContext = hubContext;
            _roomService = roomService;
        }

        [HttpPost("update_temperature")]
        public ActionResult UpdateTemperature(GetTemperatureDto temp)
        {
			Console.WriteLine(temp.Value);
			hubContext.Clients.All.SendAsync("TemperatureUpdate", temp.Value);
            return Ok("Started temperature updates.");
        }

		[HttpPost("/test")]
        public ActionResult Test(GetTemperatureDto data)
        {
            Console.WriteLine($"Id: {data.Id}, Value: {data.Value}");
            return Ok("Test");
        }

        [HttpGet("all")]
        public async Task<ActionResult<ServiceResponse<List<GetRoomDto>>>> Get()
        {
            var uid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await Task.Delay(300);
            return Ok(await _roomService.GetAllRooms(int.Parse(uid)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRoomDto>>> GetSingle(int id)
        {
            return Ok(await _roomService.GetRoomById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetRoomDto>>>> AddRoom(AddRoomDto room)
        {
            await Task.Delay(3000);
            return Ok(await _roomService.AddRoom(room));
        }


        [HttpPatch]
        public async Task<ActionResult<ServiceResponse<List<GetRoomDto>>>> UpdateRoom(UpdateRoomDto room)
        {
            Console.WriteLine("Updating room");
            var res = await _roomService.UpdateRoom(room);
            if (res.Data is null)
            {
                return NotFound(res);
            }
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRoomDto>>> DeleteRoom(int id)
        {
            var res = await _roomService.DeleteRoom(id);
            if (res.Data is null)
            {
                return NotFound(res);
            }
            return Ok(res);
        }

    }
}