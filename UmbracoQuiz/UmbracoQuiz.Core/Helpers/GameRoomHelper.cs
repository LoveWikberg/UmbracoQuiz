using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.PublishedModels;
using Umbraco.Web.Security;
using UmbracoQuiz.Core.Helpers.Interfaces;
using UmbracoQuiz.Core.Models;

namespace UmbracoQuiz.Core.Helpers
{
    public class GameRoomHelper : IGameRoomHelper
    {
        public int CreateRoom(CreateRoomModel model, IContentService contentService, IPublicAccessService publicAccessService, UmbracoHelper umbracoHelper, MembershipHelper Members)
        {
            var gameRoomListPageId = umbracoHelper.ContentAtRoot().First().Children.First(x => x.ContentType.Alias.ToLower() == nameof(GameListPage).ToLower()).Id;

            var roomToCreate = contentService.Create(model.Name, gameRoomListPageId, nameof(GameRoomPage));

            Range<decimal> playerRange = new Range<decimal>
            {
                Minimum = model.MinPlayers,
                Maximum = model.MaxPlayers
            };
            roomToCreate.SetValue(nameof(GameRoomPage.MaxNumberOfPlayers), playerRange);
            roomToCreate.SetValue(nameof(GameRoomPage.RoomCode), GeneratePassword(4));

            contentService.SaveAndPublish(roomToCreate);

            var loginPageId = umbracoHelper.ContentAtRoot().First().Children.First(x => x.ContentType.Alias.ToLower() == nameof(LoginPage).ToLower()).Id;
            var loginPage = contentService.GetById(loginPageId);
            var entry = new PublicAccessEntry(
                                   roomToCreate,
                                   loginPage,
                                   loginPage,
                                   new List<PublicAccessRule>());
            publicAccessService.Save(entry);

            publicAccessService.AddRule(roomToCreate, "MemberUsername", Members.CurrentUserName);

            return roomToCreate.Id;
        }


        private string GeneratePassword(int length)
        {
            string characters = "ABCDEFGHIJKLMONPQRSTUVXYZ123456789";
            StringBuilder password = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int characterIndex = random.Next(0, characters.Length - 1);
                password.Append(characters[characterIndex]);
            }
            return password.ToString();
        }
    }
}
