using System;
namespace IOT_Backend.Models
{
	public class Room
	{
		public long Id { get; set; }
		public string? Title { get; set; }
		public double Area { get; set; }
		public bool HasSensor { get; set; }
	}
}