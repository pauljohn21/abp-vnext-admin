using Microsoft.AspNetCore.Authorization;
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
        protected string CreatePolicyName { get; set; }
        protected string UpdatePolicyName { get; set; }
        protected string DeletePolicyName { get; set; }

        public bool HasCreatePermission { get; set; }
        public bool HasUpdatePermission { get; set; }
        public bool HasDeletePermission { get; set; }
        protected virtual async Task SetPermissionsAsync()
        {
            if (CreatePolicyName != null)
            {
                HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
            }

            if (UpdatePolicyName != null)
            {
                HasUpdatePermission = await AuthorizationService.IsGrantedAsync(UpdatePolicyName);
            }

            if (DeletePolicyName != null)
            {
                HasDeletePermission = await AuthorizationService.IsGrantedAsync(DeletePolicyName);
            }
        }
    }
}
