using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
namespace Tchivs.Abp.BootstrapUI.Components
{
    public interface ICrudBase<TAppService, TGetOutputDto,
              TGetListOutputDto,
              TKey,
              TGetListInput,
              TCreateInput,
              TUpdateInput> where TAppService : ICrudAppService<
              TGetOutputDto,
              TGetListOutputDto,
              TKey,
              TGetListInput,
              TCreateInput,
              TUpdateInput>
              where TGetOutputDto : class, IEntityDto<TKey>, new()
              where TGetListOutputDto : class, IEntityDto<TKey>, new()
    {
        /// <summary>
        /// 远程接口服务
        /// </summary>
        TAppService AppService { get; }
        /// <summary>
        /// 是否有添加权限
        /// </summary>
        bool HasCreatePermission { get; }
        /// <summary>
        /// 是否有编辑权限
        /// </summary>
        bool HasUpdatePermission { get; }
        /// <summary>
        /// 是否有删除权限
        /// </summary>
        bool HasDeletePermission { get; }
    }
}
