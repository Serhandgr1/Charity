﻿namespace AssociationWebApp.Models
{
	public class SignInViewModel
	{
		public string UserName { get; set; } = default!;
		public string Password { get; set; } = default!;
		public bool RememberMe { get; set; } = true;
	}
}
