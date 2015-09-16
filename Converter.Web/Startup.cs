using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Converter.Web.Startup))]
namespace Converter.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
