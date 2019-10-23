using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web.PublishedModels;
using UmbracoQuiz.Core.Helpers.Interfaces;
using UmbracoQuiz.Core.Models;

namespace UmbracoQuiz.Core.Controllers
{
    public class GameRoomController : SurfaceController
    {
        private readonly IGameRoomHelper _gameRoomHelper;

        public GameRoomController(IGameRoomHelper gameRoomHelper)
        {
            _gameRoomHelper = gameRoomHelper;
        }

        [HttpPost]
        public ActionResult CreateRoom(CreateRoomModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["showModal"] = true;
                TempData["createRoomModel"] = model;
                return CurrentUmbracoPage();
            }

            //var gameRoomListPageId = Umbraco.ContentAtRoot().First().Children.First(x => x.ContentType.Alias.ToLower() == nameof(GameListPage).ToLower()).Id;

            //var roomToCreate = Services.ContentService.Create(model.Name, gameRoomListPageId, nameof(GameRoomPage));

            //Range<decimal> playerRange = new Range<decimal>
            //{
            //    Minimum = model.MinPlayers,
            //    Maximum = model.MaxPlayers
            //};
            //roomToCreate.SetValue(nameof(GameRoomPage.MaxNumberOfPlayers), playerRange);
            //roomToCreate.SetValue(nameof(GameRoomPage.RoomCode), _gameRoomHelper.GeneratePassword(4));

            //var loginPageId = Umbraco.ContentAtRoot().First().Children.First(x => x.ContentType.Alias.ToLower() == nameof(LoginPage).ToLower()).Id;
            //var loginPage = Services.ContentService.GetById(loginPageId);
            //if (!Services.PublicAccessService.IsProtected(loginPage))
            //{
            //    var entry = new PublicAccessEntry(
            //                           roomToCreate,
            //                           loginPage,
            //                           loginPage,
            //                           new List<PublicAccessRule>());
            //    Services.PublicAccessService.Save(entry);
            //}

            //Services.PublicAccessService.AddRule(roomToCreate, "MemberUsername", Members.CurrentUserName);

            //Services.ContentService.SaveAndPublish(roomToCreate);
            int createdRoomId = _gameRoomHelper.CreateRoom(model, Services.ContentService, Services.PublicAccessService, Umbraco, Members);
            GameRoomPage createdRoom = (GameRoomPage)Services.ContentService.GetById(createdRoomId);

            return RedirectToAction(nameof(EnterRoom), new { nodeId = createdRoom.Id, roomCode = createdRoom.RoomCode });
        }

        [HttpGet]
        public ActionResult EnterRoom(int nodeId, string roomCode)
        {
            var room = Services.ContentService.GetById(nodeId);
            if (roomCode == room.GetValue<string>(nameof(GameRoomPage.RoomCode)))
            {
                //var loginPageId = Umbraco.ContentAtRoot().First().Children.First(x => x.ContentType.Alias.ToLower() == nameof(LoginPage).ToLower()).Id;
                //var loginPage = Services.ContentService.GetById(loginPageId);
                //if (!Services.PublicAccessService.IsProtected(loginPage))
                //{
                //    var entry = new PublicAccessEntry(
                //                           room,
                //                           loginPage,
                //                           loginPage,
                //                           new List<PublicAccessRule>());
                //    Services.PublicAccessService.Save(entry);
                //}

                Services.PublicAccessService.AddRule(room, "MemberUsername", Members.CurrentUserName);
                return RedirectToUmbracoPage(room.Id);
            }

            ModelState.AddModelError(nodeId.ToString(), "Felaktig rumkod");
            return CurrentUmbracoPage();
        }

    }
}
