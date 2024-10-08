﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
	public class Question
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public List<Answer> Answers { get; set; } = new List<Answer>();
		public int? SurveyId { get; set; }
	}
}
