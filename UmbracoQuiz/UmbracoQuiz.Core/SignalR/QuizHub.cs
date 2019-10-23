using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UmbracoQuiz.Core.Controllers;
using UmbracoQuiz.Core.Helpers;
using UmbracoQuiz.Core.Helpers.Interfaces;
using UmbracoQuiz.Core.Models;
using UmbracoQuiz.Core.Models.Enums;
using RazorEngine;
using RazorEngine.Templating;
using RazorEngine.Configuration;
using System.Reflection;
using System.IO;
using System.Web.Hosting;
using UmbracoQuiz.Core.Models.Constants;

namespace UmbracoQuiz.Core.SignalR
{
    public class QuizHub : Hub
    {
        private readonly IRazorHelper _razorHelper;
        private readonly IQuizHubHelper _quizHubHelper;
        public QuizHub(IRazorHelper razorHelper, IQuizHubHelper quizHubHelper)
        {
            _razorHelper = razorHelper;
            _quizHubHelper = quizHubHelper;
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var diconnectedPlayer = HttpRuntime.Cache.GetEnumerator();

            return base.OnDisconnected(stopCalled);
        }

        public void ConnectedToBuzzQuizRoom(string roomId)
        {
            _quizHubHelper.AddPlayerToRoomCache(
                roomId, Context.ConnectionId, Context.User.Identity.Name, PlayerRole.Participator, out BuzzerQuizRoomModel roomModel);
            Groups.Add(Context.ConnectionId, roomId);

            string roomLobbyView = _razorHelper.RazorViewToString(PartialViewConstants.RoomLobby, roomModel);

            Clients.Group(roomId).playerJoin(roomLobbyView);
        }

        public void ConnectedToAlternativeQuizRoom(string roomId)
        {
            _quizHubHelper.AddPlayerToRoomCache(
                roomId, Context.ConnectionId, Context.User.Identity.Name, PlayerRole.Participator, out AlternativeQuizRoomModel roomModel);
            Groups.Add(Context.ConnectionId, roomId);

            string roomLobbyView = _razorHelper.RazorViewToString(PartialViewConstants.RoomLobby, roomModel);

            Clients.Group(roomModel.RoomName).playerJoin(roomLobbyView);
        }

        public void ShowBuzzerQuestion(string roomId, BuzzerQuizRoomModel roomModel = null)
        {
            roomModel = roomModel ?? (BuzzerQuizRoomModel)HttpRuntime.Cache.Get(roomId);
            var currentQuestion = roomModel.Questions.FirstOrDefault(q => !q.IsAnswered);
            string buzzerQuestionView = _razorHelper.RazorViewToString(PartialViewConstants.BuzzQuestion, currentQuestion);

            var referee = roomModel.Players.FirstOrDefault(p => p.Role == PlayerRole.GameMaster && p.IsConnected);

            Clients.Group(roomId).showBuzzerQuestion(buzzerQuestionView);
        }

        public void PlayerBuzz(string roomId)
        {
            BuzzerQuizRoomModel roomModel = (BuzzerQuizRoomModel)HttpRuntime.Cache.Get(roomId);
            if (roomModel.PlayerHasBuzzed)
                return;

            roomModel.PlayerHasBuzzed = true;
            //HttpRuntime.Cache.Insert(roomId, roomModel, null, DateTime.Now.AddMinutes(180), System.Web.Caching.Cache.NoSlidingExpiration);


            var gameMaster = _quizHubHelper.GetGameMaster(roomModel.Players);
            if (gameMaster == null)
            {
                // Pause game if game master isn't connected and show message
                Clients.Group(roomId).gameMasterIsDisconnected();
            }

            var buzzedPlayer = roomModel.Players.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId);
            buzzedPlayer.IsCurrentBuzzer = true;

            string playerBuzzedView = _razorHelper.RazorViewToString(PartialViewConstants.PlayerBuzzed, buzzedPlayer.Name);
            string determinePlayerAnswerView = _razorHelper.RazorViewToString(PartialViewConstants.DeterminePlayerAnswer, Context.User.Identity.Name);

            _quizHubHelper.SetQuizRoomCache(roomId, roomModel);
            Clients.Client(gameMaster.ConnectionId).determineIfAnswerIsCorrect(determinePlayerAnswerView);
            Clients.Group(roomId, gameMaster.ConnectionId).letPlayerAnswer(playerBuzzedView);
        }

        public void CorrectAnswer(string roomId, int pointsGiven)
        {
            BuzzerQuizRoomModel roomModel = _quizHubHelper.GetBuzzerQuizRoomFromCache(roomId);
            var buzzedPlayer = roomModel.Players.FirstOrDefault(p => p.IsCurrentBuzzer);
            buzzedPlayer.Points += pointsGiven;
            var currentQuestion = roomModel.Questions.First(q => !q.IsAnswered);
            currentQuestion.IsAnswered = true;

            var gameMaster = _quizHubHelper.GetGameMaster(roomModel.Players);

            string showAnswerResultView = _razorHelper.RazorViewToString(PartialViewConstants.ShowAnswerResult, pointsGiven);

            Clients.Group(roomId, gameMaster.ConnectionId).showAnswerResult(showAnswerResultView);

            // Create a timer with a two second interval.
            var timer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Elapsed += delegate
            {
                ShowBuzzerQuestion(roomId);
                timer.Dispose();
            };
            timer.Start();
        }

    }
}
