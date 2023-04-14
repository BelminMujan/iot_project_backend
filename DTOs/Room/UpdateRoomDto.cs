using System;
namespace IOT_Backend.DTOs.Room
{
	public class UpdateRoomDto
	{
        public long Id { get; set; }
        public string? Title { get; set; }
        public double xAxis { get; set; }
        public double yAxis { get; set; }
        public bool HasSensor { get; set; }
        public string SensorId {get; set; } = String.Empty;

    }
}

