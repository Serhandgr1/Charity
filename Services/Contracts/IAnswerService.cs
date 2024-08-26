using Entities.Model;
using Entities.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IAnswerService
    {
        Task<List<AnswerDto>> GetAllAnswersByQuestionId(int questionId);
        Task AddAnswer(AnswerDto answerDto);
        Task DeleteAnswer(int id);
    }
}
