using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UmbracoQuiz.Core.Helpers.Interfaces
{
    public interface IRazorHelper
    {
        /// <summary>
        /// Converts razor view to string. The given view has to be added in Startup.cs
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        string RazorViewToString(string viewName, object model);

    }
}
