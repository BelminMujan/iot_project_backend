using System;
namespace IOT_Backend.Models
{
	public class Room
	{
		public long Id { get; set; }
		public string? Title { get; set; }
		public double xAxis { get; set; }
		public double yAxis { get; set; }
		public bool HasSensor { get; set; }
		public int UserId { get; set; }
		public User? User { get; set; }
	}
}