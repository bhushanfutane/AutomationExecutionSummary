using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AutomationExecutionSummary.Startup))]
namespace AutomationExecutionSummary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
