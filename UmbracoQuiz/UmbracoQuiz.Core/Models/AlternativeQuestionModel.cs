using System.Collections.Generic;

namespace UmbracoQuiz.Core.Models
{
    public class AlternativeQuestionModel : QuestionModel
    {
        public List<string> Alternatives { get; set; }
    }
}
