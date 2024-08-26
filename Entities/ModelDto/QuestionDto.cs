using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<AnswerDto>? Answers { get; set; } 
        public int? SurveyId { get; set; }
    }
}
