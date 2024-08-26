using Entities.Model;
using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IQuestionService
    {
        Task<List<QuestionDto>> GetAllQuestions();
        Task<List<SurveyDto>> GetAllSurvey();
        Task<QuestionDto> GetQuestionById(int id);
        Task AddQuestion(SurveyDto surveyDto, List<QuestionDto> questionDtos);
        Task UpdateQuestion(QuestionDto questionDto);
        Task DeleteQuestion(int id);
        Task<List<QuestionDto>> GetQuestionBySurveyId(int id);
        Task DeleteSurvey(int id);
    }

}
