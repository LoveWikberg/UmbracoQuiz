using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoQuiz.Core.Models;

namespace UmbracoQuiz.Core.Helpers.Interfaces
{
    public interface IQuizHubHelper
    {
        RoomModel AddPlayerToRoomCache(string roomId, string playerConnectionId, string playerName, PlayerRole playerRole);
    }
}
