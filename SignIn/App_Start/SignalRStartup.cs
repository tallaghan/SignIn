using Owin;
using Microsoft.Owin;
using SignIn;
[assembly: OwinStartup(typeof(Startup))]

namespace SignIn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}