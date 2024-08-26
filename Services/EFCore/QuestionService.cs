using AutoMapper;
using Entities.Model;
using Entities.ModelDto;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EFCore
{
    public class QuestionService : IQuestionService
    {
        private readonly IRepositoryQuestion _questionRepository;
        private readonly IRepositorySurvey _surveyRepository;
        private readonly IRepositoryAnswer _answerRepository;
        private readonly RepositoryContext _context;
        private readonly IMapper _mapper;
        public QuestionService(IRepositoryQuestion questionRepository, IRepositorySurvey surveyRepository, IMapper mapper, IRepositoryAnswer answerRepository, RepositoryContext context)
        {
            _questionRepository = questionRepository;
            _surveyRepository = surveyRepository;
            _mapper = mapper;
            _answerRepository = answerRepository;
            _context = context;
        }

        public async Task<List<QuestionDto>> GetAllQuestions()
        {
            var questions = await _questionRepository.Read(false);
            return questions.Select(q => new QuestionDto { Id = q.Id, Text = q.Text }).ToList();
        }
        public async Task<List<SurveyDto>> GetAllSurvey()
        {
            var questions = await _surveyRepository.Read(false);
            return questions.Select(q => new SurveyDto { Id = q.Id, Name = q.Name }).ToList();
        }
        public async Task<QuestionDto> GetQuestionById(int id)
        {
            var question = await _questionRepository.GetById(id);
            if (question == null) return null;
            return new QuestionDto { Id = question.Id, Text = question.Text };
        }
        public async Task<List<QuestionDto>> GetQuestionBySurveyId(int id)    
        {
            var question = await _questionRepository.GetBySurveyId(id);
            var slider = _mapper.Map<List<QuestionDto>>(question);
            if (question == null) return null;
            return slider;
        }

        public async Task AddQuestion(SurveyDto surveyDto, List<QuestionDto> questionDtos)
        {
            Survey survey = new Survey();
            List<Question> ques = new List<Question>();
            survey.Name = surveyDto.Name;
            await _surveyRepository.Create(survey);
            var srv = await _surveyRepository.GetByName(surveyDto.Name);
            foreach (var item in questionDtos)
            {
                ques.Add(new Question { SurveyId = srv.Id, Text = item.Text });
            }
            foreach (var item in ques)
            {
                await _questionRepository.Create(item);
            }
            
        }

        public async Task UpdateQuestion(QuestionDto questionDto)
        {
            var question = await _questionRepository.GetById(questionDto.Id);
            if (question != null)
            {
                question.Text = questionDto.Text;
                await _questionRepository.Update(question);
            }
        }

        public async Task DeleteQuestion(int id)
        {
            var question = await _questionRepository.GetById(id);
            if (question != null)
            {
                await _questionRepository.Delete(question);
            }
        }

        public async Task DeleteSurvey(int id)
        {
            // Önce ankete bağlı tüm soruları ve cevapları alalım
            var questions = await _context.Questions
                .Where(q => q.SurveyId == id)
                .ToListAsync();

            if (questions.Any())
            {
                // Her sorunun cevaplarını sil
                foreach (var question in questions)
                {
                    var answers = await _context.Answers
                        .Where(a => a.QuestionId == question.Id)
                        .ToListAsync();

                    if (answers.Any())
                    {
                        _context.Answers.RemoveRange(answers);
                    }
                }

                // Soruları sil
                _context.Questions.RemoveRange(questions);
            }

            // Ankete bağlı sorular silindikten sonra anketi sil
            var survey = await _context.Surveys
                .FirstOrDefaultAsync(s => s.Id == id);

            if (survey != null)
            {
                _context.Surveys.Remove(survey);
            }

            // Değişiklikleri veritabanına kaydet
            await _context.SaveChangesAsync();
        }
    }
}
