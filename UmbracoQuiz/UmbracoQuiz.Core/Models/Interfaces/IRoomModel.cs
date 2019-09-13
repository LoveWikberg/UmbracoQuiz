using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoQuiz.Core.Models.Interfaces
{
    public interface IRoomModel<T>
    {
        string RoomName { get; set; }
        List<Player> Players { get; set; }
    }
}
