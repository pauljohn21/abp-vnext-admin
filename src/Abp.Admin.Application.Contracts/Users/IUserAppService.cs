using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Admin.Menus;
using Volo.Abp.Application.Services;

namespace Abp.Admin.Users
{
    public interface IUserAppService:IApplicationService
    {
        Task<ICollection<MenuDto>> GetMenus();
    }
}