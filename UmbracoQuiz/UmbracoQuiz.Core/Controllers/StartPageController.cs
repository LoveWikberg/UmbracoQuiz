using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace UmbracoQuiz.Core.Controllers
{
    public class StartPageController : RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            return base.Index(model);
        }
    }
}
