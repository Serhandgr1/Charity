using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace AssociationWebApp.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly IMemberShipService _memberShipService;

        public MemberShipController(IMemberShipService memberShipService)
        {
            _memberShipService = memberShipService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberShipDto memberShipDto)
        {
            if (ModelState.IsValid)
            {
                await _memberShipService.CreateMemberShip(memberShipDto);
                return RedirectToAction("Index", "Home");
            }
            return View(memberShipDto);
        }
    }
}
