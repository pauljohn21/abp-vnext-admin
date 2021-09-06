using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Authorization.Permissions;
using Abp.Admin.Menus;
using Abp.Admin.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Abp.Admin.Blazor.Menus
{
    public class AdminMenuContributor : IMenuContributor
    {
        public AdminMenuContributor()
        {
        }


        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
          //  IDbMenuManager dbMenuManager = context.ServiceProvider.GetRequiredService<IDbMenuManager>();
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        void Add(MenuConfigurationContext context, ICollection<MenuDto> menus, IStringLocalizer localizer)
        {
            foreach (var menuDto in menus)
            {
                var m = new ApplicationMenuItem(menuDto.Name, displayName: localizer[menuDto.Name], menuDto.Url,
                    icon: menuDto.Icon);
                context.Menu.AddItem(m);
                if (m.Items.Any())
                {
                    Add(context, menuDto.Menus, localizer);
                }
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(AdminMenus.Prefix, displayName: "Admin", "/Admin",
                icon: "fa fa-globe"));
            //  context.Menu.AddItem(new ApplicationMenuItem(AdminMenus.Menu, displayName: "Menu", "/Admin/Menu", icon: "fa fa-menu"));

            var res = context.GetLocalizer<AdminResource>();
            var administrationMenu = context.Menu.GetAdministration();
            // var menus = await dbMenuManager.GetMenus();
            // Add(context, menus, res);
            #region Identity
            //Identity
            var l = context.GetLocalizer<IdentityResource>();
            var identityMenuItem = new ApplicationMenuItem(IdentityMenuNames.GroupName, l["Menu:IdentityManagement"],
                icon: "far fa-id-card");
            administrationMenu.AddItem(identityMenuItem);
            identityMenuItem.AddItem(new ApplicationMenuItem(
                IdentityMenuNames.Roles,
                l["Roles"],
                url: "/identity/roles").RequirePermissions(IdentityPermissions.Roles.Default));
            identityMenuItem.AddItem(new ApplicationMenuItem(
                IdentityMenuNames.Users,
                l["Users"],
                url: "/identity/users").RequirePermissions(IdentityPermissions.Users.Default));

            #endregion
        }
    }
}