﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
	public class User :IdentityUser
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
