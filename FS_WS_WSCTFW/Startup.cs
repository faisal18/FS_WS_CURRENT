using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FS_WS_WSCTFW.Startup))]
namespace FS_WS_WSCTFW
{
    public partial class Startup {
        public void Configuration(IAppBuilder app1) {
            ConfigureAuth(app1);
        }
    }
}
