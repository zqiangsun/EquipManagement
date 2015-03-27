using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EquipManagement.Startup))]
namespace EquipManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
