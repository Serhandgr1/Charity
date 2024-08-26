using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enum
{
	public enum BlogTypes:int
	{
        [Display(Name = "Causes")]
        Causes =1,
        [Display(Name = "Events")]
        Events =2,
        [Display(Name = "Blogs")]
        Blogs =3
	}
}
