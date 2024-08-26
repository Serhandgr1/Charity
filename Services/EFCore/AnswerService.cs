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
    public class AnswerService : IAnswerService
    {
        private readonly IRepositoryAnswer _answerRepository;
        private readonly IRepositoryManager _repository;

        public AnswerService(IRepositoryAnswer answerRepository, IRepositoryManager repository)
        {
            _answerRepository = answerRepository;
            _repository = repository;
        }

        public async Task<List<AnswerDto>> GetAllAnswersByQuestionId(int questionId)
        {
            var answers = await _answerRepository.GetAllAnswersByQuestionId(questionId);
            return answers.Select(a => new AnswerDto { Id = a.Id, Text = a.Text, QuestionId = a.QuestionId }).ToList();
        }

        public async Task AddAnswer(AnswerDto answerDto)
        {
            var answer = new Answer
            {
                Text = answerDto.Text,
                QuestionId = answerDto.QuestionId
            };
            await _answerRepository.Create(answer);
        }

        public async Task DeleteAnswer(int id)  
        {
            var answer = _repository.Answer.GetById(id).Result;
            if (answer != null)
            {
                await _repository.Answer.Delete(answer);
                _repository.Save();
            }
        }
    }

}
