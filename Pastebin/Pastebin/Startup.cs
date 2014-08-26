using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pastebin.Startup))]
namespace Pastebin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
