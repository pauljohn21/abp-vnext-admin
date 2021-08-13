using Abp.Admin.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.Admin.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class AdminController : AbpController
    {
        protected AdminController()
        {
            LocalizationResource = typeof(AdminResource);
        }
    }
}