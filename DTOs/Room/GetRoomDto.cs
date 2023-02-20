using System;
namespace IOT_Backend.DTOs.Room
{
	public class GetRoomDto
	{
        public long Id { get; set; }
        public string? Title { get; set; }
        public double Area { get; set; }
        public bool HasSensor { get; set; }
    }
}

