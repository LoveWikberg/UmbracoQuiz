using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;

namespace UmbracoQuiz.Core.Models
{
    public class GameListPageViewModel : ContentModel
    {
        public GameListPageViewModel(IPublishedContent content) : base(content) { }
        public CreateRoomModel CreateRoomModel { get; set; }

    }
}
