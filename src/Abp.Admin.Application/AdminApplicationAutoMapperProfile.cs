using Abp.Admin.Menus;
using AutoMapper;

namespace Abp.Admin
{
    public class AdminApplicationAutoMapperProfile : Profile
    {
        public AdminApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Menu, MenuDto>();
            CreateMap<CreateUpdateMenuDto, MenuDto>(MemberList.Source);
        }
    }
}
