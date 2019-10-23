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
        public void AddPlayerToRoomCache<T>(string roomId, string playerConnectionId, string playerName, PlayerRole playerRole, out T roomModel) where T : IRoomModel<T>, new()
        {
            Player newPlayer = new Player
            {
                ConnectionId = playerConnectionId,
                Name = playerName,
                Role = playerRole
            };
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

            SetQuizRoomCache(roomId, roomModel);
        }

        public Player GetGameMaster(List<Player> players)
        {
            return players.FirstOrDefault(p => p.Role == PlayerRole.GameMaster);
        }

        public BuzzerQuizRoomModel GetBuzzerQuizRoomFromCache(string roomId)
        {
            return (BuzzerQuizRoomModel)HttpRuntime.Cache.Get(roomId);
        }

        public void SetQuizRoomCache<T>(string key, IRoomModel<T> data)
        {
            HttpRuntime.Cache.Insert(key, data, null, DateTime.Now.AddMinutes(180), System.Web.Caching.Cache.NoSlidingExpiration);
        }

    }
}
