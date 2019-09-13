using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoQuiz.Core.Models.Interfaces;

namespace UmbracoQuiz.Core.Models
{
    public class BuzzerQuizRoomModel : RoomModel, IRoomModel<BuzzerQuizRoomModel>
    {
        public bool PlayerHasBuzzed { get; set; }
        public List<QuestionModel> Questions { get; set; }
    }
}
