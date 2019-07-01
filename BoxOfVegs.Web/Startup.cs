using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoxOfVegs.Web.Startup))]
namespace BoxOfVegs.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
