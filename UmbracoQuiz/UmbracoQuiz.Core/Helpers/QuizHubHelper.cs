using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UmbracoQuiz.Core.Helpers.Interfaces;
using UmbracoQuiz.Core.Models;

namespace UmbracoQuiz.Core.Helpers
{
    public class QuizHubHelper : IQuizHubHelper
    {
        public RoomModel AddPlayerToRoomCache(string roomId, string playerConnectionId, string playerName, PlayerRole playerRole)
        {
            Player newPlayer = new Player
            {
                ConnectionId = playerConnectionId,
                Name = playerName,
                Role = playerRole
            };
            RoomModel roomModel;
            if (HttpRuntime.Cache.Get(roomId) != null)
            {
                roomModel = (RoomModel)HttpRuntime.Cache.Get(roomId);
                roomModel.Players.Add(newPlayer);
            }
            else
            {
                roomModel = new RoomModel
                {
                    RoomName = roomId,
                    Players = new List<Player>
                    {
                      newPlayer
                    }
                };
            }
            HttpRuntime.Cache.Insert(roomId, roomModel, null, DateTime.Now.AddMinutes(180), System.Web.Caching.Cache.NoSlidingExpiration);
            return roomModel;
        }

    }
}
