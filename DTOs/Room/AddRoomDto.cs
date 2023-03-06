using System;
namespace IOT_Backend.DTOs.Room
{
	public class AddRoomDto
	{
        public string? Title { get; set; }
        public double xAxis { get; set; }
        public double yAxis { get; set; }
        public bool HasSensor { get; set; }
    }
}

