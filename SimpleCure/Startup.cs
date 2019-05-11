using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleCure.Startup))]
namespace SimpleCure
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
