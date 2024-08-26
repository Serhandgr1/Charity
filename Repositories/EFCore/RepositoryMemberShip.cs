using Entities.Model;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
	public class RepositoryMemberShip:RepositoryBase<MemberShip>,IRepositoryMemberShip
	{
		private readonly RepositoryContext _context;
        public RepositoryMemberShip(RepositoryContext context): base(context) 
        {
            _context = context;
        }
    }
}
