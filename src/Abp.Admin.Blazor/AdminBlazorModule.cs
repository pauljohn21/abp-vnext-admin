using Microsoft.Extensions.DependencyInjection;
using Abp.Admin.Blazor.Menus;
using Tchivs.Abp.AspNetCore.Components.Web.Theming;
using Tchivs.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Identity;
using Volo.Abp.Threading;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.ObjectExtending;

namespace Abp.Admin.Blazor
{
    [DependsOn(
        typeof(AdminApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpAspNetCoreComponentsWebThemingModule),
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
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            OneTimeRunner.Run(() =>
            {
                ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToUi(
                        IdentityModuleExtensionConsts.ModuleName,
                        IdentityModuleExtensionConsts.EntityNames.Role,
                        createFormTypes: new[] { typeof(IdentityRoleCreateDto) },
                        editFormTypes: new[] { typeof(IdentityRoleUpdateDto) }
                    );

                ModuleExtensionConfigurationHelper
                    .ApplyEntityConfigurationToUi(
                        IdentityModuleExtensionConsts.ModuleName,
                        IdentityModuleExtensionConsts.EntityNames.User,
                        createFormTypes: new[] { typeof(IdentityUserCreateDto) },
                        editFormTypes: new[] { typeof(IdentityUserUpdateDto) }
                    );
            });
        }
    }
}