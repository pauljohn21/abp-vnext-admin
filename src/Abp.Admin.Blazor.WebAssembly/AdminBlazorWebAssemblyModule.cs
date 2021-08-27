using Tchivs.Abp.AspNetCore.Components.WebAssembly.Theming;
using Tchivs.Abp.Identity.Blazor.WebAssembly;
using Volo.Abp.Modularity;

namespace Abp.Admin.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AdminBlazorModule),
        typeof(AdminHttpApiClientModule),
        typeof(IdentityBlazorWebAssemblyModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class AdminBlazorWebAssemblyModule : AbpModule
    {
        
    }
}