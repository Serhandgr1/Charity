using Entities.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace AssociationWebApp.Models
{
    public class SurveyViewModel
    {
        public string SurveyName { get; set; } 
        public string Question { get; set; }
    }
}
