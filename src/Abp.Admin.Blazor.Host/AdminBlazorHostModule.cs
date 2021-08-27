using System;
using System.Net.Http;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Abp.Admin.Blazor.WebAssembly;
using Tchivs.Abp.AspNetCore.Components.Web.BasicTheme.Themes.Bootstrap;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Tchivs.Abp.AspNetCore.Components.WebAssembly.BasicTheme;
using Tchivs.Abp.AspNetCore.Components.Web.Theming.Routing;

namespace Abp.Admin.Blazor.Host
{
[DependsOn(
        typeof(AbpAutofacWebAssemblyModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyBasicThemeModule),
        //typeof(AbpAccountBlazorModule),
        //typeof(AbpIdentityBlazorWebAssemblyModule),
        typeof(AdminBlazorWebAssemblyModule)
    )]
    public class AdminBlazorHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
            var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

            ConfigureAuthentication(builder);
            ConfigureHttpClient(context, environment);
            ConfigureBlazorise(context);
            ConfigureRouter(context);
            ConfigureUI(builder);
            ConfigureMenu(context);
            ConfigureAutoMapper(context);
        }

        private void ConfigureRouter(ServiceConfigurationContext context)
        {
            Configure<AbpRouterOptions>(options =>
            {
               // options.AdditionalAssemblies.Add(typeof(AdminBlazorHostModule).Assembly);
                options.AppAssembly = typeof(AdminBlazorHostModule).Assembly;
            });
        }

        private void ConfigureMenu(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new AdminHostMenuContributor());
            });
        }

        private void ConfigureBlazorise(ServiceConfigurationContext context)
        {
        }

        private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("AuthServer", options.ProviderOptions);
                options.ProviderOptions.DefaultScopes.Add("Admin");
            });
        }

        private static void ConfigureUI(WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("#ApplicationContainer");
        }

        private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
        {
            context.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri(environment.BaseAddress)
            });
        }

        private void ConfigureAutoMapper(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AdminBlazorHostModule>();
            });
        }
    }
}
