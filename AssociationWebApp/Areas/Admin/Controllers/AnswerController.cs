using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AnswerController : Controller
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;

        public AnswerController(IAnswerService answerService, IQuestionService questionService)
        {
            _answerService = answerService;
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> Show(int questionId)
        {
            var answers = await _answerService.GetAllAnswersByQuestionId(questionId);
            ViewData["QuestionId"] = questionId;
            return View(answers);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _answerService.DeleteAnswer(id);
            return RedirectToAction("Show","Question");
        }
    }
}

