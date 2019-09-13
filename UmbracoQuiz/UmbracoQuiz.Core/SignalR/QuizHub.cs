using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UmbracoQuiz.Core.Helpers.Interfaces;
using UmbracoQuiz.Core.Models;
using UmbracoQuiz.Core.Models.Enums;

namespace UmbracoQuiz.Core.SignalR
{
    public class QuizHub : Hub
    {
        private readonly IPartialViewHelper _partialViewHelper;
        private readonly IQuizHubHelper _quizHubHelper;
        public QuizHub(IPartialViewHelper partialViewHelper, IQuizHubHelper quizHubHelper)
        {
            _partialViewHelper = partialViewHelper;
            _quizHubHelper = quizHubHelper;
        }

        public void ConnectedToBuzzQuizRoom(string roomId)
        {
            BuzzerQuizRoomModel roomModel = _quizHubHelper.AddPlayerToRoomCache<BuzzerQuizRoomModel>(
                roomId, Context.ConnectionId, Context.User.Identity.Name, PlayerRole.Participator);
            Groups.Add(Context.ConnectionId, roomId);

            string roomLobbyView = _partialViewHelper.RazorViewToString("_roomLobby", roomModel);
            Clients.Group(roomModel.RoomName).playerJoin(roomLobbyView);
        }

        public void ConnectedToAlternativeQuizRoom(string roomId)
        {
            AlternativeQuizRoomModel roomModel = _quizHubHelper.AddPlayerToRoomCache<AlternativeQuizRoomModel>(
                roomId, Context.ConnectionId, Context.User.Identity.Name, PlayerRole.Participator);
            Groups.Add(Context.ConnectionId, roomId);

            string roomLobbyView = _partialViewHelper.RazorViewToString("_roomLobby", roomModel);
            Clients.Group(roomModel.RoomName).playerJoin(roomLobbyView);
        }

        public void PlayerBuzz(string roomId)
        {
            BuzzerQuizRoomModel roomModel = (BuzzerQuizRoomModel)HttpRuntime.Cache.Get(roomId);
            if (roomModel.PlayerHasBuzzed)
                return;

            roomModel.PlayerHasBuzzed = true;
            HttpRuntime.Cache.Insert(roomId, roomModel, null, DateTime.Now.AddMinutes(180), System.Web.Caching.Cache.NoSlidingExpiration);

            var referee = roomModel.Players.FirstOrDefault(p => p.Role == PlayerRole.GameMaster && p.IsConnected);
            if(referee == null)
            {
                // Pause game if gamemaster isn't connected and show message
            }
            string roomLobbyView = _partialViewHelper.RazorViewToString("_playerBuzzed", Context.User.Identity.Name);

            Clients.Client(referee.ConnectionId).letPlayerAnswer(roomLobbyView);
            Clients.Group(roomId, referee.ConnectionId).letPlayerAnswer(roomLobbyView);
        }


    }
}
