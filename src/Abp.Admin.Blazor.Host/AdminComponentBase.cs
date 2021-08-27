using Abp.Admin.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Abp.Admin.Blazor
{
    public abstract class AdminComponentBase : AbpComponentBase
    {
        protected AdminComponentBase()
        {
            LocalizationResource = typeof(AdminResource);
        }
    }
}
