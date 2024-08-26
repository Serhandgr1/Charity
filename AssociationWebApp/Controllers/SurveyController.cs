using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Entities.ModelDto;
using Services.Contracts;

namespace AssociationWebApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;

        public SurveyController(IQuestionService questionService, IAnswerService answerService)
        {
            _questionService = questionService;
            _answerService = answerService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var questions = await _questionService.GetAllSurvey();
            return View(questions);
        }

        [HttpGet]
        public async Task<IActionResult> Answer(int questionId)
        {
            var answers = await _questionService.GetQuestionBySurveyId(questionId);
            ViewData["QuestionId"] = questionId;
            return View(answers);
        }

        [HttpPost]
        public async Task<IActionResult> Answer([FromForm]int questionId, List<AnswerDto> answerDto)
        {
            foreach (var answer in answerDto)
            {
                if (ModelState.IsValid)
                {
                    await _answerService.AddAnswer(answer);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
