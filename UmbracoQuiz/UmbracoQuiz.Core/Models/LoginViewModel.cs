using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoQuiz.Core.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ange användarnamn")]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Ange lösenord")]
        [Display(Name = "Lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string RedirectUrl { get; set; }
    }
}
