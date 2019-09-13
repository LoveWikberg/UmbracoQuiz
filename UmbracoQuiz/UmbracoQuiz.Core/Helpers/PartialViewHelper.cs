using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using UmbracoQuiz.Core.Helpers.Interfaces;

namespace UmbracoQuiz.Core.Helpers
{
    public class PartialViewHelper : IPartialViewHelper
    {
        public string RazorViewToString(string viewName, object model, ControllerContext controllerContext = null)
        {
            if (controllerContext == null)
            {
                var httpContext = HttpContext.Current;
                if (httpContext == null)
                    throw new NotSupportedException("An HTTP context is required to render the partial view to a string");
                var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
                var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);
                controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);
            }
            controllerContext.Controller.ViewData.Model = model;

            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}
