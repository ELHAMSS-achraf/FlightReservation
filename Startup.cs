using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Reservation3.Startup))]
namespace Reservation3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
