using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using RazorEngine;
using RazorEngine.Templating;
using System.IO;
using System.Web.Hosting;
using Umbraco.Web;
using UmbracoQuiz.Core.Helpers;
using UmbracoQuiz.Core.Models.Constants;
using UmbracoQuiz.Core.SignalR;

[assembly: OwinStartup("CustomStartup", typeof(UmbracoQuiz.Core.Initiation.Startup))]

namespace UmbracoQuiz.Core.Initiation
{
    public class Startup : UmbracoDefaultOwinStartup
    {
        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);

            GlobalHost.DependencyResolver.Register(
                typeof(QuizHub),
                () => new QuizHub(new RazorHelper(), new QuizHubHelper()));

            app.MapSignalR();

            // Razor views
            string pathToPartialViews = $@"{HostingEnvironment.ApplicationPhysicalPath}\Views\Partials\";
            Engine.Razor.AddTemplate(PartialViewConstants.RoomLobby, File.ReadAllText(pathToPartialViews + PartialViewConstants.RoomLobby));
            Engine.Razor.AddTemplate(PartialViewConstants.BuzzQuestion, File.ReadAllText(pathToPartialViews + PartialViewConstants.BuzzQuestion));
            Engine.Razor.AddTemplate(PartialViewConstants.PlayerBuzzed, File.ReadAllText(pathToPartialViews + PartialViewConstants.PlayerBuzzed));
            Engine.Razor.AddTemplate(PartialViewConstants.DeterminePlayerAnswer, File.ReadAllText(pathToPartialViews + PartialViewConstants.DeterminePlayerAnswer));
            Engine.Razor.AddTemplate(PartialViewConstants.ShowAnswerResult, File.ReadAllText(pathToPartialViews + PartialViewConstants.ShowAnswerResult));

        }
    }
}
