using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoQuiz.Core.Models.Interfaces;

namespace UmbracoQuiz.Core.Models
{
    public class AlternativeQuizRoomModel : RoomModel, IRoomModel<AlternativeQuizRoomModel>
    {
        List<AlternativeQuestionModel> Questions { get; set; }
    }
}
