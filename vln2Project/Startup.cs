using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(h37.Startup))]
namespace h37
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
