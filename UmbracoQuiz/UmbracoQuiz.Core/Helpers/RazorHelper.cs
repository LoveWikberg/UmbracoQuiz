using RazorEngine;
using RazorEngine.Templating;
using System;
using UmbracoQuiz.Core.Helpers.Interfaces;
using UmbracoQuiz.Core.Models.Constants;

namespace UmbracoQuiz.Core.Helpers
{
    public class RazorHelper : IRazorHelper
    {
        /// <summary>
        /// Converts razor view to string. The given view has to be added in Startup.cs
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public string RazorViewToString(string viewName, object model)
        {
            bool isTemplateCached = Engine.Razor.IsTemplateCached(viewName, model.GetType());
            if (isTemplateCached)
                throw new Exception("The given template has not been cached. See Startup.cs.");

            return Engine.Razor.RunCompile(PartialViewConstants.RoomLobby, model.GetType(), model);
        }
    }
}
