using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Core.Services.Implement;
using Umbraco.Web.Mvc;
using UmbracoQuiz.Core.Models;

namespace UmbracoQuiz.Core.Controllers
{
    public class MemberController : SurfaceController
    {
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();
            if (Members.Login(viewModel.UserName, viewModel.Password))
                return RedirectToCurrentUmbracoPage();
            else
            {
                ModelState.AddModelError(string.Empty, "Användarnamn eller lösenord är felaktigt");
                return CurrentUmbracoPage();
            }
        }
        public ActionResult Logout()
        {
            Members.Logout();
            return RedirectToCurrentUmbracoPage();
        }
    }
}
