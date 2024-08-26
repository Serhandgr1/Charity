using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ModelDto
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; } 
    }
}
