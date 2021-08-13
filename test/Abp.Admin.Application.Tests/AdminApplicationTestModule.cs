using Volo.Abp.Modularity;

namespace Abp.Admin
{
    [DependsOn(
        typeof(AdminApplicationModule),
        typeof(AdminDomainTestModule)
        )]
    public class AdminApplicationTestModule : AbpModule
    {

    }
}