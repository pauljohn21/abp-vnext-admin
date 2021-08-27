using Microsoft.Extensions.DependencyInjection;
using Abp.Admin.Blazor.Menus;
using Tchivs.Abp.AspNetCore.Components.Web.Theming;
using Tchivs.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Tchivs.Abp.Identity.Blazor;

namespace Abp.Admin.Blazor
{
    [DependsOn(
        typeof(AdminApplicationContractsModule),
        typeof(AbpAspNetCoreComponentsWebThemingModule),
        typeof(IdentityBlazorModule),
        typeof(AbpAutoMapperModule)
        )]
    public class AdminBlazorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<AdminBlazorModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<AdminBlazorAutoMapperProfile>(validate: true);
            });

            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new AdminMenuContributor());
            });

            Configure<AbpRouterOptions>(options =>
            {
                options.AdditionalAssemblies.Add(typeof(AdminBlazorModule).Assembly);
            });
        }
    }
}