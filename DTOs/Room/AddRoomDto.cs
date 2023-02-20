using System;
namespace IOT_Backend.DTOs.Room
{
	public class AddRoomDto
	{
        public string? Title { get; set; }
        public double Area { get; set; }
        public bool HasSensor { get; set; }
    }
}

