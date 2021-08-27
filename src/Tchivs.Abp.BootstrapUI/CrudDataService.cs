using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.Application.Dtos;

namespace Tchivs.Abp.BootstrapUI
{
    public abstract class CrudDataService<TAppService,TEntityDto, TKey> : IDataService<TEntityDto> where TEntityDto : class, IEntityDto<TKey>, new()
    {
        [Inject] protected TAppService AppService { get; set; }
        [Inject] protected IStringLocalizer<AbpUiResource> UiLocalizer { get; set; }
        protected TKey EditingEntityId;

        public Task<bool> AddAsync(TEntityDto model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(IEnumerable<TEntityDto> models)
        {
            throw new NotImplementedException();
        }

        public Task<QueryData<TEntityDto>> QueryAsync(QueryPageOptions option)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync(TEntityDto model)
        {
            throw new NotImplementedException();
        }
    }
}
