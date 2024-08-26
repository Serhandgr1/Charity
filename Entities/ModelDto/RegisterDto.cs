using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public record RegisterDto
    {
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public ICollection<string>? Roles { get; set; }
    }
}
