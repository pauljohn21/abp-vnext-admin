using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components;

namespace Abp.Admin.Blazor.Pages
{
    public partial class Index 
    {
        [Inject] public NavigationManager NavigationManager { get; set; }
        protected override void OnInitialized()
        {
        }
    }
}
