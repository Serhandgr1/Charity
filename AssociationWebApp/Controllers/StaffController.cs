using AutoMapper;
using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace AssociationWebApp.Controllers
{
    public class StaffController : Controller
    {
        private readonly IRepositoryStaff _repository; // Repository arayüzü
        private readonly IMapper _mapper; // AutoMapper

        public StaffController(IRepositoryStaff repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // Tüm staff'ları al
            var staffs = await _repository.Read(false);
            // Staff'ları DTO'ya dönüştür
            var staffDtos = _mapper.Map<List<StaffDto>>(staffs);

            // DTO listesini view'e gönder
            return View(staffDtos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var staffDto = await GetStaffById(id);
            if (staffDto == null)
            {
                return NotFound();
            }

            return View(staffDto);
        }

        public async Task<StaffDto> GetStaffById(int id)
        {
            var staff = await _repository.GetById(id);
            return _mapper.Map<StaffDto>(staff);
        }
    }
}
