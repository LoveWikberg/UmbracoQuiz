using System.Collections.Generic;

namespace UmbracoQuiz.Core.Models
{
    public class AlternativeQuestionViewModel : QuestionViewModel
    {
        public List<string> Alternatives { get; set; }
    }
}
