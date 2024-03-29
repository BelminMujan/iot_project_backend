﻿using System;
namespace IOT_Backend.Models
{
	public class ServiceResponse<T>
	{
		public T? Data { get; set; }
		public bool Success { get; set; } = true;
		public string Message { get; set; } = string.Empty;
   	 	public string? Token { get; set; }
	}
}

