using Tchivs.Abp.AspNetCore.Components.Web.Theming;
using Tchivs.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.Web.BasicTheme
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsWebThemingModule)
        )]
    public class AbpAspNetCoreComponentsWebBasicThemeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureRouter(context);
        }
        private void ConfigureRouter(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
                options.AppAssembly = typeof(AbpAspNetCoreComponentsWebBasicThemeModule).Assembly;
            });
        }
    }
}