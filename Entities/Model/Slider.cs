using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
	public class Slider:BaseEntity
	{
        public string Title { get; set; }
        public string  Entry { get; set; }
        public string Tag { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
    }
}
