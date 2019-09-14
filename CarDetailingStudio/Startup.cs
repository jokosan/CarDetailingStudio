using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarDetailingStudio.Startup))]
namespace CarDetailingStudio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
