using AutoMapper;
using Entities.Model;
using Entities.ModelDto;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{

	public class MemberShipService : IMemberShipService
	{
		private readonly IRepositoryManager _repository;
		private readonly IMapper _mapper;
		public MemberShipService(IRepositoryManager repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateMemberShip(MemberShipDto memberShipDto)
		{
			var memberShip = _mapper.Map<MemberShip>(memberShipDto);
			await _repository.MemberShip.Create(memberShip);
			_repository.Save();
		}

		public async Task DeleteMemberShip(int id)
		{
			var memberShip = _repository.MemberShip.GetById(id).Result;
			if (memberShip != null)
			{
				await _repository.MemberShip.Delete(memberShip);
				_repository.Save();
			}
		}

        public async Task<MemberShipDto> GetMemberShipById(int id)
        {
            var memberShip = await _repository.MemberShip.GetById(id);
            if (memberShip == null)
            {
                throw new KeyNotFoundException($"MemberShip with ID {id} not found.");
            }
            return _mapper.Map<MemberShipDto>(memberShip);
        }

        public async Task<List<MemberShipDto>> GetMemberShipList()
		{
			var memberShips = await _repository.MemberShip.Read(false);
			return _mapper.Map<List<MemberShipDto>>(memberShips);
		}

		public async Task UpdateMemberShip(MemberShipDto memberShipDto)
		{
			var memberShip = _mapper.Map<MemberShip>(memberShipDto);
			await _repository.MemberShip.Update(memberShip);
			_repository.Save();
		}


	}
}
