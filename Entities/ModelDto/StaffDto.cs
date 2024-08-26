using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
	public class StaffDto 
	{
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public IFormFile? FormFile { get; set; }
        public string ContentKr { get; set; }
        public string ContentTr { get; set; }

    }
}
