using Tchivs.Abp.BootstrapUI;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Tchivs.Abp.AspNetCore.Components.Web.Theming
{
    [DependsOn(
        typeof(BootstrapUIModule),
        typeof(AbpUiNavigationModule)
        )]
    public class AbpAspNetCoreComponentsWebThemingModule : AbpModule
    {
    }
}