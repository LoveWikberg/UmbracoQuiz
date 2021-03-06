﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;
using UmbracoQuiz.Core.Models;
using UmbracoQuiz.Core.Models.Enums;

namespace UmbracoQuiz.Core.Controllers
{
    public class GameListPageController : RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            GameListPageViewModel viewModel = new GameListPageViewModel(model.Content)
            {
                CreateRoomModel = TempData["createRoomModel"] != null ? (CreateRoomModel)TempData["createRoomModel"] : new CreateRoomModel()
            };
            viewModel.CreateRoomModel.Quizes = model.Content.Parent.Descendants<BuzzQuizPage>().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
                //Id = x.Id.ToString(),
                //Name = x.Name,
                //Description = x.Description,
                //TypeOfQuiz = QuizType.Buzz
            }).ToList();

            viewModel.CreateRoomModel.Quizes.AddRange(
                model.Content.Parent.Descendants<AlternativeQuizPage>().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                    //Id = x.Id.ToString(),
                    //Name = x.Name,
                    //TypeOfQuiz = QuizType.Alternative
                }).ToList());

            return base.Index(viewModel);
        }
    }
}
