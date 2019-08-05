using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoQuiz.Core.Models
{
    public class RoomModel
    {
        public string RoomName { get; set; }
        public List<Player> Players { get; set; }
    }
    public class Player
    {
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public PlayerRole Role { get; set; }

    }
    public enum PlayerRole
    {
        Participator = 0,
        Referee = 1,
        AllScreen = 2
    }
}
