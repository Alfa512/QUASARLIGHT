using Microsoft.Owin;
using Owin;
using QuasarLight.UI;

[assembly: OwinStartup(typeof(Startup))]
namespace QuasarLight.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}