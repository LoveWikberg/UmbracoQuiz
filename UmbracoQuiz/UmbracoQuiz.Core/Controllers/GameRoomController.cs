using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;
using UmbracoQuiz.Core.Models;

namespace UmbracoQuiz.Core.Controllers
{
    public class GameRoomController : SurfaceController
    {
        [HttpGet]
        public ActionResult CreateRoom(/*CreateRoomModel model*/)
        {
            if (!ModelState.IsValid)
            {
                TempData["showModal"] = true;
                return CurrentUmbracoPage();
            }
            return CurrentUmbracoPage();
        }

        [HttpGet]
        public ActionResult EnterRoom(int nodeId, string roomCode)
        {
            GameRoomPage gameRoomPageModel;
            var room = Services.ContentService.GetById(nodeId);
            if (roomCode == room.GetValue<string>(nameof(gameRoomPageModel.RoomCode)))
            {
                var loginPageId = Umbraco.ContentAtRoot().First().Children.First().Id;
                var loginPage = Services.ContentService.GetById(loginPageId);
                if (!Services.PublicAccessService.IsProtected(room))
                {
                    var entry = new PublicAccessEntry(
                                           room,
                                           loginPage,
                                           loginPage,
                                           new List<PublicAccessRule>());
                    Services.PublicAccessService.Save(entry);
                }

                Services.PublicAccessService.AddRule(room, "MemberUsername", Members.CurrentUserName);
                return RedirectToUmbracoPage(room.Id);
            }

            ModelState.AddModelError(nodeId.ToString(), "Felaktig rumkod");
            return CurrentUmbracoPage();
        }
    }
}
