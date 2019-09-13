﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace UmbracoQuiz.Core.Helpers.Interfaces
{
    public interface IPartialViewHelper
    {
        string RazorViewToString(string viewName, object model, ControllerContext controllerContext = null);
    }
}
