﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Nem.Models
{
	public abstract class User
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public int Zipcode { get; set; }
	}
}
