using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
	public class StaffAccount:BaseEntity
	{
        public int AccountType { get; set; }
        public string Account { get; set; }
    }
}
