using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Abp.Admin.Blazor.Host
{
    public class AdminHostMenuContributor : IMenuContributor
    {
        public Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if(context.Menu.DisplayName != StandardMenus.Main)
            {
                return Task.CompletedTask;
            }

            var l = context.GetLocalizer<Abp.Admin.Localization.AdminResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "Admin.Home",
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            return Task.CompletedTask;
        }
    }
}
