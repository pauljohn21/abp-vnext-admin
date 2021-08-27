using System.Reflection;

namespace Tchivs.Abp.AspNetCore.Components.Web.Theming.Routing
{
    public class AbpRouterOptions
    {
        public Assembly AppAssembly { get; set; }

        public RouterAssemblyList AdditionalAssemblies { get; }

        public AbpRouterOptions()
        {
            AdditionalAssemblies = new RouterAssemblyList();
        }
    }
}
