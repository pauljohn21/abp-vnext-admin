
using Tchivs.Abp.AspNetCore.Components.Server.BasicTheme.Bundling;
using Tchivs.Abp.AspNetCore.Components.Server.Theming;
using Tchivs.Abp.AspNetCore.Components.Server.Theming.Bundling;
using Tchivs.Abp.AspNetCore.Components.Web.BasicTheme;
using Tchivs.Abp.AspNetCore.Components.Web.Theming.Toolbars;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.Modularity;

namespace Tchivs.Abp.AspNetCore.Components.Server.BasicTheme
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsWebBasicThemeModule),
        typeof(AbpAspNetCoreComponentsServerThemingModule)
        )]
    public class AbpAspNetCoreComponentsServerBasicThemeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpToolbarOptions>(options =>
            {
                options.Contributors.Add(new BasicThemeToolbarContributor());
            });
            
            Configure<AbpBundlingOptions>(options =>
            {
                options
                    .StyleBundles
                    .Add(BlazorBasicThemeBundles.Styles.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(BlazorStandardBundles.Styles.Global)
                            .AddContributors(typeof(BlazorBasicThemeStyleContributor));
                    });

                options
                    .ScriptBundles
                    .Add(BlazorBasicThemeBundles.Scripts.Global, bundle =>
                    {
                        bundle
                            .AddBaseBundles(BlazorStandardBundles.Scripts.Global)
                            .AddContributors(typeof(BlazorBasicThemeScriptContributor));
                    });
            });
        }
    }
}
