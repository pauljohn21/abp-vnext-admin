using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Linq;

namespace Abp.Admin.Menus
{
   
    [Authorize]
    public class MenuAppService :
        CrudAppService<Menu, MenuDto, Guid, PagedAndIncludeSortedResultRequestDto, CreateUpdateMenuDto,
            CreateUpdateMenuDto>, IMenuAppService
    {
        protected override string CreatePolicyName => Permissions.AdminPermissions.MenuPermissions.Create;
        protected override string DeletePolicyName => Permissions.AdminPermissions.MenuPermissions.Delete;
        protected override string UpdatePolicyName=> Permissions.AdminPermissions.MenuPermissions.Update;
        protected override string GetListPolicyName=> Permissions.AdminPermissions.MenuPermissions.Default;
        protected override string GetPolicyName=> Permissions.AdminPermissions.MenuPermissions.Default;

        public MenuAppService(IMenuRepository repository) : base(repository)
        {
        }


        protected override async Task<IQueryable<Menu>> CreateFilteredQueryAsync(
            PagedAndIncludeSortedResultRequestDto input)
        {
            IQueryable<Menu> query = null;
            if (input.Include)
            {
                query = await this.ReadOnlyRepository.WithDetailsAsync(x => x.Menus);
                query = query.Where(x => !x.ParentId.HasValue);
            }
            else
            {
                query = await this.ReadOnlyRepository.GetQueryableAsync();
            }

            return query;
        }
    }
}