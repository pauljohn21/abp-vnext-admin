using System.Threading.Tasks;

namespace Tchivs.Abp.AspNetCore.Components.Web.Theming.Toolbars
{
    public interface IToolbarContributor
    {
        Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
    }
}