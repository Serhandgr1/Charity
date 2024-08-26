using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
	public class SliderDto
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleTr { get; set; }
        public string TitleKr { get; set; }
        public string  Entry { get; set; }
        public string Tag { get; set; }
        public string Image { get; set; }
        public IFormFile? FormFile { get; set; }
        public string Content { get; set; }
        public string ContentTr { get; set; }
        public string ContentKr { get; set; }
    }
}
