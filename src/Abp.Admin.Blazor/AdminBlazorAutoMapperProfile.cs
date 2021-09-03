using AutoMapper;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;

namespace Abp.Admin.Blazor
{
    public class AdminBlazorAutoMapperProfile : Profile
    {
        public AdminBlazorAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<IdentityUserDto, IdentityUserUpdateDto>()
              .MapExtraProperties()
              .Ignore(x => x.Password)
              .Ignore(x => x.RoleNames);

            CreateMap<IdentityRoleDto, IdentityRoleUpdateDto>()
                .MapExtraProperties();
        }
    }
}