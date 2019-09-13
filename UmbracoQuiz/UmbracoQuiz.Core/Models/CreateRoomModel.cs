using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Web.PublishedModels;
using UmbracoQuiz.Core.Models.Enums;

namespace UmbracoQuiz.Core.Models
{
    public class CreateRoomModel
    {
        [Required]
        [Display(Name = "Rummets namn")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Minst antal spelare för att starta spelet")]
        public int MinPlayers { get; set; }
        [Required]
        [Display(Name = "Max antal spelare")]
        public int MaxPlayers { get; set; }
        [Required]
        [Display(Name = "Välj quiz")]
        public string SelectedQuizId { get; set; }
        public List<Quiz> Quizes { get; set; }
    }

    public class Quiz
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public QuizType TypeOfQuiz { get; set; }
    }
}
