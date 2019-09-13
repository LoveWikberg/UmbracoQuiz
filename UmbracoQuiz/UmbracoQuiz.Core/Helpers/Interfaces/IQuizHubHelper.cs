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
        T AddPlayerToRoomCache<T>(string roomId, string playerConnectionId, string playerName, PlayerRole playerRole) where T : IRoomModel<T>, new();
    }
}
