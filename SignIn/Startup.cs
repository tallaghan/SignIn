using Microsoft.Owin;
using Owin;
using SignIn;

namespace SignIn
{
    public class SignalRStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}