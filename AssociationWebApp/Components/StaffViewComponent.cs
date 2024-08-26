using AutoMapper;
using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssociationWebApp.Components
{
    public class StaffViewComponent : ViewComponent
    {
        private readonly IRepositoryStaff _repository;
        private readonly IMapper _mapper;

        public StaffViewComponent(IRepositoryStaff repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var staffs = await _repository.Read(false);
            var staffDtos = _mapper.Map<List<StaffDto>>(staffs);
            return View(staffDtos);
        }
    }
}
