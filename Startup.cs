using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmailManager.Startup))]
namespace EmailManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
