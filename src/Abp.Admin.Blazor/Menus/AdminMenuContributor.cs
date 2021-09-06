using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Abp.Admin.Blazor.Menus
{
    public class AdminMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
          
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(AdminMenus.Prefix, displayName: "Admin", "/Admin", icon: "fa fa-globe"));
            context.Menu.AddItem(new ApplicationMenuItem(AdminMenus.Menu, displayName: "Menu", "/Admin/Menu", icon: "fa fa-menu"));
            var administrationMenu = context.Menu.GetAdministration();

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

            return Task.CompletedTask;
        }
    }
}