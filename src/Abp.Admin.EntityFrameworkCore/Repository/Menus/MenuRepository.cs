using System;
using Abp.Admin.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Abp.Admin.Menus;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.Admin.Repository.Menus
{
   
  public  class MenuRepository: EfCoreRepository<AdminDbContext,Menu,Guid>,IMenuRepository
    {
        public MenuRepository(IDbContextProvider<AdminDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
