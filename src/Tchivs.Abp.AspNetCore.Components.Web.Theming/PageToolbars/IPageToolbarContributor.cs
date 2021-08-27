using System.Threading.Tasks;

namespace Tchivs.Abp.AspNetCore.Components.Web.Theming.PageToolbars
{
    public interface IPageToolbarContributor
    {
        Task ContributeAsync(PageToolbarContributionContext context);
    }
}
