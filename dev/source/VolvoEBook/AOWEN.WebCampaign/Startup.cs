using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AOWEN.WebCampaign.Startup))]
namespace AOWEN.WebCampaign
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
