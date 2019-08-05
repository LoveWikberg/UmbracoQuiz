using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UmbracoQuiz.Core.Helpers.Interfaces;
using UmbracoQuiz.Core.Models;

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

        public void ConnectedToRoom(string roomId)
        {
            RoomModel roomModel = _quizHubHelper.AddPlayerToRoomCache(
                roomId, Context.ConnectionId, "Lotta pruttar", PlayerRole.Participator);
            Groups.Add(Context.ConnectionId, roomId);
            Clients.Group(roomModel.RoomName).playerJoin(roomModel);
        }

        public void Send(string user, string message)
        {
            Clients.All.addNewMessageToPage(user, message);
        }
    }
}
