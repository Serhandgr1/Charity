using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.EFCore;

namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MemberShipController : Controller
    {
        private readonly IMemberShipService _manager;

        public MemberShipController(IMemberShipService manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Show()
        {
            var memberShipList = await _manager.GetMemberShipList();
            return View(memberShipList);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _manager.GetMemberShipById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] MemberShipDto memberShipDto)
        {
            if (ModelState.IsValid)
            {
                await _manager.UpdateMemberShip(memberShipDto);
                return RedirectToAction("Show");
            }

            return View(memberShipDto);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _manager.DeleteMemberShip(id);
            return RedirectToAction("Show", "MemberShip");
        }
    }
}
