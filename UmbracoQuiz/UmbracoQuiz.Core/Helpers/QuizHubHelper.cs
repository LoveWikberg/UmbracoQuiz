using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UmbracoQuiz.Core.Helpers.Interfaces;
using UmbracoQuiz.Core.Models;
using UmbracoQuiz.Core.Models.Enums;
using UmbracoQuiz.Core.Models.Interfaces;

namespace UmbracoQuiz.Core.Helpers
{
    public class QuizHubHelper : IQuizHubHelper
    {
        public T AddPlayerToRoomCache<T>(string roomId, string playerConnectionId, string playerName, PlayerRole playerRole) where T : IRoomModel<T>, new()
        {
            Player newPlayer = new Player
            {
                ConnectionId = playerConnectionId,
                Name = playerName,
                Role = playerRole
            };
            T roomModel;
            if (HttpRuntime.Cache.Get(roomId) != null)
            {
                roomModel = (T)HttpRuntime.Cache.Get(roomId);
                roomModel.Players.Add(newPlayer);
            }
            else
            {
                roomModel = new T
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
