using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoQuiz.Core.Models.Enums;
using UmbracoQuiz.Core.Models.Interfaces;

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
        public int Points { get; set; }
        public PlayerRole Role { get; set; }
        public bool IsPlayerBlocked { get; set; }
        public bool IsConnected { get; set; }
        public bool IsCurrentBuzzer { get; set; }

    }
   
}
