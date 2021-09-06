using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Linq;
using Volo.Abp.Uow;

namespace Abp.Admin.Menus
{
    public class MenuDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRepository<Menu, Guid> _repository;
        private readonly IAsyncQueryableExecuter _queryableExecuter;
        public MenuDataSeedContributor(IGuidGenerator guidGenerator, IRepository<Menu, Guid> repository, IAsyncQueryableExecuter queryableExecuter)
        {
            this._guidGenerator = guidGenerator;
            this._repository = repository; this._queryableExecuter = queryableExecuter;
        }
        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            if (!await _repository.AnyAsync())
            {
                Menu systemMenu = new Menu(this._guidGenerator.Create(), "System", "/system", "ion:settings-outline",  999,  MenuType.Menu);
                systemMenu.AddChildrenMenu(new Menu(this._guidGenerator.Create(), "User", "/admin/system/user", "",  1,  MenuType.Path));
                systemMenu.AddChildrenMenu(new Menu(this._guidGenerator.Create(), "Role", "/admin/system/user", "",  2,  MenuType.Path));
                systemMenu.AddChildrenMenu(new Menu(this._guidGenerator.Create(), "Menu", "/admin/menu", "",  3,  MenuType.Path));
                await this._repository.InsertAsync(systemMenu, true);
            }
        }
    }
}
