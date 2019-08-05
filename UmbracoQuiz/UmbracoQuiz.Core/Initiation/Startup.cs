using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Umbraco.Web;
using UmbracoQuiz.Core.Helpers;
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
        () => new QuizHub(new PartialViewHelper(), new QuizHubHelper()));

            app.MapSignalR();
        }
    }
}
