using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodieSocial.Startup))]
namespace FoodieSocial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
