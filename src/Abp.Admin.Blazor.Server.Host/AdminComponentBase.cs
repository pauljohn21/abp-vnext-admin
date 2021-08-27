using Abp.Admin.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Abp.Admin.Blazor.Server.Host
{
    public abstract class AdminComponentBase : AbpComponentBase
    {
        protected AdminComponentBase()
        {
            LocalizationResource = typeof(AdminResource);
        }
    }
}
