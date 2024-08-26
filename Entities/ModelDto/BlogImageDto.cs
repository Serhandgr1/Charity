using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
	public class BlogImageDto
	{
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Image { get; set; }
        public IFormFile? FormFile { get; set; }
    }
}
