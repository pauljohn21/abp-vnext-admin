using System.Threading.Tasks;

namespace Abp.Admin.Data
{
    public interface IAdminDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
