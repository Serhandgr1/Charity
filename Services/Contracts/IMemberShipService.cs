using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
	public interface IMemberShipService
	{
		Task<List<MemberShipDto>> GetMemberShipList();
		Task<MemberShipDto> GetMemberShipById(int id);
		Task CreateMemberShip(MemberShipDto memberShipDto);
		Task UpdateMemberShip(MemberShipDto memberShipDto);
		Task DeleteMemberShip(int id);
	}
}
