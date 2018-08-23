using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CPQaunt2.APP.Startup))]
namespace CPQaunt2.APP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
