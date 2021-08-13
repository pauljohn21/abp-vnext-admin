using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Abp.Admin
{
    [Dependency(ReplaceServices = true)]
    public class AdminBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Admin";
    }
}
