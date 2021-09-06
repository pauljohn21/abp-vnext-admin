using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Admin.Menus;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using System;
namespace Abp.Admin.Users
{
    public class UserAppService:ApplicationService,IUserAppService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IRepository<RoleMenu, Guid> _repository;

        public UserAppService(IMenuRepository menuRepository,
            IRepository<RoleMenu,Guid>repository)
        {
            _menuRepository = menuRepository;
            _repository = repository;
        }
        public async Task<ICollection<MenuDto>> GetMenus()
        {
            throw new Exception();
        }
    }
}