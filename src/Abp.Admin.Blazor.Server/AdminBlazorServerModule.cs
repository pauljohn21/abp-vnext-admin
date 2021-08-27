using Tchivs.Abp.AspNetCore.Components.Server.Theming;
using Tchivs.Abp.Identity.Blazor.Server;
using Volo.Abp.Modularity;

namespace Abp.Admin.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(IdentityBlazorServerModule),
        typeof(AdminBlazorModule)
        )]
    public class AdminBlazorServerModule : AbpModule
    {
        
    }
}