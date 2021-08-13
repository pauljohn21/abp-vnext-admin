using Abp.Admin.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.Admin
{
    [DependsOn(
        typeof(AdminEntityFrameworkCoreTestModule)
        )]
    public class AdminDomainTestModule : AbpModule
    {

    }
}