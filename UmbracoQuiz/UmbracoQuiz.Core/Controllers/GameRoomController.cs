using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;

namespace UmbracoQuiz.Core.Controllers
{
    public class GameRoomController : SurfaceController
    {
        [HttpGet]
        public ActionResult EnterRoom(int nodeId, string roomCode)
        {
            var room = (GameRoomPage)Umbraco.Content(nodeId);
            if (roomCode == room.RoomCode)
            {
                return RedirectToUmbracoPage(room);
            }

            ModelState.AddModelError(nodeId.ToString(), "Felaktig rumkod");
            return CurrentUmbracoPage();
        }
    }
}
