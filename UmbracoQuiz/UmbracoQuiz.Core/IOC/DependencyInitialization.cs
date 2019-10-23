using Umbraco.Core;
using Umbraco.Core.Composing;
using UmbracoQuiz.Core.Helpers;
using UmbracoQuiz.Core.Helpers.Interfaces;

namespace UmbracoQuiz.Core.IOC
{
    public class DependencyInitialization : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IGameRoomHelper, GameRoomHelper>();
        }
    }
}
