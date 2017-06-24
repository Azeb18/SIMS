using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SIMS.Models;

[assembly: OwinStartupAttribute(typeof(SIMS.Startup))]
namespace SIMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);            
        }


    }


}
