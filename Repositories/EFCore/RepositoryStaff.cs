using Entities.Model;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
	public class RepositoryStaff:RepositoryBase<Staff>,IRepositoryStaff
	{
		private readonly RepositoryContext _context;
        public RepositoryStaff(RepositoryContext context):base(context)
        {
            _context = context;
        }
    }
}
