using Tchivs.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Abp.Admin.Blazor.WebAssembly
{
    [DependsOn(
        typeof(AdminBlazorModule),
        typeof(AdminHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class AdminBlazorWebAssemblyModule : AbpModule
    {
        
    }
}