using System.Threading.Tasks;
using Tchivs.Abp.AspNetCore.Components.Server.BasicTheme.Themes.Bootstrap;
using Tchivs.Abp.AspNetCore.Components.Web.Theming.Toolbars;

namespace Tchivs.Abp.AspNetCore.Components.Server.BasicTheme
{
    public class BasicThemeToolbarContributor : IToolbarContributor
    {
        public Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            if (context.Toolbar.Name == StandardToolbars.Main)
            {
                context.Toolbar.Items.Add(new ToolbarItem(typeof(LanguageSwitch)));
                context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginDisplay)));
            }

            return Task.CompletedTask;
        }
    }
}
