using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Tchivs.Abp.AspNetCore.Components.Web.Theming.Toolbars;

namespace Tchivs.Abp.AspNetCore.Components.Web.BasicTheme.Themes.Bootstrap
{
    public partial class NavToolbar
    {
        [Inject]
        private IToolbarManager ToolbarManager { get; set; }

        private List<RenderFragment> ToolbarItemRenders { get; set; } = new List<RenderFragment>();

        protected override async Task OnInitializedAsync()
        {
            var toolbar = await ToolbarManager.GetAsync(StandardToolbars.Main);

            ToolbarItemRenders.Clear();

            var sequence = 0;
            foreach (var item in toolbar.Items)
            {
                ToolbarItemRenders.Add(builder =>
                {
                    builder.OpenComponent(sequence++, item.ComponentType);
                    builder.CloseComponent();
                });
            }
        }

    }
}
