using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tchivs.Abp.BootstrapUI.Components
{
    public abstract partial class CrudPageBase<
                  TAppService,
                  TGetOutputDto,
                  TGetListOutputDto,
                  TKey,
                  TGetListInput,
                  TCreateInput,
                  TUpdateInput,
                  TCreateViewModel,
                  TUpdateViewModel>
              : AbpComponentBaseEx
              where TAppService : ICrudAppService<
                  TGetOutputDto,
                  TGetListOutputDto,
                  TKey,
                  TGetListInput,
                  TCreateInput,
                  TUpdateInput>
              where TGetOutputDto : class, IEntityDto<TKey>, new()
              where TGetListOutputDto : class, IEntityDto<TKey>, new()
              where TCreateInput : class
              where TUpdateInput : class
              where TGetListInput : new()
      
              where TCreateViewModel : class, new()
              where TUpdateViewModel : class, new()
    {

        private IReadOnlyList<TGetListOutputDto> MapToListViewModel(IReadOnlyList<TGetListOutputDto> dtos)
        {
            if (typeof(TGetListOutputDto) == typeof(TGetListOutputDto))
            {
                return dtos.As<IReadOnlyList<TGetListOutputDto>>();
            }

            return ObjectMapper.Map<IReadOnlyList<TGetListOutputDto>, List<TGetListOutputDto>>(dtos);
        }

        protected virtual TUpdateViewModel MapToEditingEntity(TGetOutputDto entityDto)
        {
            return ObjectMapper.Map<TGetOutputDto, TUpdateViewModel>(entityDto);
        }

        protected virtual TCreateInput MapToCreateInput(TCreateViewModel createViewModel)
        {
            if (typeof(TCreateInput) == typeof(TCreateViewModel))
            {
                return createViewModel.As<TCreateInput>();
            }

            return ObjectMapper.Map<TCreateViewModel, TCreateInput>(createViewModel);
        }

        protected virtual TUpdateInput MapToUpdateInput(TUpdateViewModel updateViewModel)
        {
            if (typeof(TUpdateInput) == typeof(TUpdateViewModel))
            {
                return updateViewModel.As<TUpdateInput>();
            }

            return ObjectMapper.Map<TUpdateViewModel, TUpdateInput>(updateViewModel);
        }

    }
}
