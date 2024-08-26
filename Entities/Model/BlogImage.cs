using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
	public class BlogImage : BaseEntity
	{
        public int BlogId { get; set; }
        public string Image { get; set; }
    }
}
