using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoQuiz.Core.Models;
using UmbracoQuiz.Core.Models.Enums;
using UmbracoQuiz.Core.Models.Interfaces;

namespace UmbracoQuiz.Core.Helpers.Interfaces
{
    public interface IQuizHubHelper
    {
        void AddPlayerToRoomCache<T>(string roomId, string playerConnectionId, string playerName, PlayerRole playerRole, out T roomModel) where T : IRoomModel<T>, new();
        Player GetGameMaster(List<Player> players);
        BuzzerQuizRoomModel GetBuzzerQuizRoomFromCache(string roomId);
        void SetQuizRoomCache<T>(string key, IRoomModel<T> data);
    }
}
