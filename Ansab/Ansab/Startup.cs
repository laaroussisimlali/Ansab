using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ansab.Startup))]
namespace Ansab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
