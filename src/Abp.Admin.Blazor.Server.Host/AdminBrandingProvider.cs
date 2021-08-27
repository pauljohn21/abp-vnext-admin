using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Abp.Admin.Blazor.Server.Host
{
    [Dependency(ReplaceServices = true)]
    public class AdminBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "后台管理系统|Server";
    }
}
