using AssociationWebApp.Models;
using Entities.Model;
using Entities.ModelDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;
using Services.Contracts;

namespace AssociationWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            var questions = await _questionService.GetAllSurvey();
            return View(questions);
        }
        [HttpGet]
        public async Task<IActionResult> Questions(int questionId)
        {
            var answers = await _questionService.GetQuestionBySurveyId(questionId);
            ViewData["QuestionId"] = questionId;
            return View(answers);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            await _questionService.DeleteQuestion(id);
            return RedirectToAction("Show", "Question");
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SurveyViewModel surveyViewModel)
        {
            List<QuestionDto> question = new List<QuestionDto>();
            var ques = surveyViewModel.Question.Split("?");
            foreach(var item in ques)
            {
                var cleanedQuestion = System.Text.RegularExpressions.Regex.Replace(item, @"\s+", " ").Trim();
                if (!String.IsNullOrEmpty(cleanedQuestion))
                {
                    QuestionDto questionDto1 = new QuestionDto();
                    questionDto1.Text = cleanedQuestion;
                    question.Add(questionDto1);
                }
               
            }
            SurveyDto survey = new SurveyDto { Name = surveyViewModel.SurveyName };
            if (ModelState.IsValid)
            {
                await _questionService.AddQuestion(survey,question);
                return RedirectToAction("Show");
            }
            return View(surveyViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var question = await _questionService.GetQuestionById(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm]QuestionDto questionDto)
        {
            if (ModelState.IsValid)
            {
                await _questionService.UpdateQuestion(questionDto);
                return RedirectToAction("Show");
            }
            return View(questionDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionService.DeleteSurvey(id);
            return RedirectToAction("Show", "Question");
        }
    }
}
