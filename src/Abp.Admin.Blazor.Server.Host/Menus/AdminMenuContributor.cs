using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Abp.Admin.Localization;
using Abp.Admin.MultiTenancy;
using Volo.Abp.Account.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Abp.Admin.Blazor.Server.Host.Menus
{
    public class AdminMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;

        public AdminMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
            else if (context.Menu.Name == StandardMenus.User)
            {
                await ConfigureUserMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var administration = context.Menu.GetAdministration();
            var l = context.GetLocalizer<AdminResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    AdminMenus.Home,
                    l["Menu:Home"],
                    "/?",
                    icon: "fas fa-home",
                    order: 0
                )
            );
                        
            //if (MultiTenancyConsts.IsEnabled)
            //{
            //    administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
            //}
            //else
            //{
            //    administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            //}
            
            //administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
            //administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

            return Task.CompletedTask;
        }
        
        private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<AdminResource>();
            var accountStringLocalizer = context.GetLocalizer<AccountResource>();
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

            var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

            if (currentUser.IsAuthenticated)
            {
                context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountStringLocalizer["ManageYourProfile"],
                    $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}", icon: "fa fa-cog", order: 1000, null, "_blank"));
                context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", l["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000));
            }

            return Task.CompletedTask;
        }
    }
}
