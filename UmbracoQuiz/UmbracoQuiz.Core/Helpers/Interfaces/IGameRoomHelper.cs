using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Security;
using UmbracoQuiz.Core.Models;

namespace UmbracoQuiz.Core.Helpers.Interfaces
{
    public interface IGameRoomHelper
    {
        int CreateRoom(CreateRoomModel model, IContentService contentService, IPublicAccessService publicAccessService, UmbracoHelper umbracoHelper, MembershipHelper Members);
    }
}
