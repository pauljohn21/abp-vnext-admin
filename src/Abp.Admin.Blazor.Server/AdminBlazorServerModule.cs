using Tchivs.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Abp.Admin.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(AdminBlazorModule)
        )]
    public class AdminBlazorServerModule : AbpModule
    {
        
    }
}