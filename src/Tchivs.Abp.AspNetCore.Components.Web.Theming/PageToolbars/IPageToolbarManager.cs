using System.Threading.Tasks;

namespace Tchivs.Abp.AspNetCore.Components.Web.Theming.PageToolbars
{
    public interface IPageToolbarManager
    {
        Task<PageToolbarItem[]> GetItemsAsync(PageToolbar toolbar);
    }
}
