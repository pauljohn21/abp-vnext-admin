using BootstrapBlazor.Components;
using JetBrains.Annotations;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;

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
        /// <summary>
        /// 远程应用服务
        /// </summary>
        [Inject]
        protected TAppService AppService { get; set; }
        /// <summary>
        /// ABPUI本地化
        /// </summary>
        [Inject] protected IStringLocalizer<AbpUiResource> UiLocalizer { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        protected TGetListInput GetListInput = new TGetListInput();
        /// <summary>
        /// 查询结果数据集合
        /// </summary>
        protected IReadOnlyList<TGetListOutputDto> Entities = Array.Empty<TGetListOutputDto>();
        /// <summary>
        /// 新建实体
        /// </summary>
        protected TCreateViewModel NewEntity;
        /// <summary>
        /// 当前更新的实体的ID
        /// </summary>
        protected TKey EditingEntityId;
        /// <summary>
        /// 更新实体
        /// </summary>
        protected TUpdateViewModel EditingEntity;
        /// <summary>
        /// 操作按钮列表
        /// </summary>
        protected EntityActionDictionary EntityActions { get; set; }
        protected TableColumnDictionary TableColumns { get; set; }

        /// <summary>
        /// 获得/设置 EditTemplate 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TUpdateViewModel> EditTemplate { get; set; }
        [Parameter]
        public RenderFragment<TCreateInput> AddTemplate { get; set; }

        protected CrudPageBase()
        {
            NewEntity = new TCreateViewModel();
            EditingEntity = new TUpdateViewModel();
            TableColumns = new TableColumnDictionary();
            EntityActions = new EntityActionDictionary();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await SetEntityActionsAsync();
            await SetTableColumnsAsync();
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await base.OnAfterRenderAsync(firstRender);
                await SetToolbarItemsAsync();
            }
        }


      



        protected virtual async Task CreateEntityAsync()
        {
            try
            {
                if (CreateValidateAll() ?? true)
                {
                    await OnCreatingEntityAsync();

                    await CheckCreatePolicyAsync();
                    var createInput = MapToCreateInput(NewEntity);
                    await AppService.CreateAsync(createInput);

                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual bool? CreateValidateAll()
        {
            throw new NotImplementedException();
        }

        protected virtual Task OnCreatingEntityAsync()
        {
            return Task.CompletedTask;
        }



        protected virtual async Task UpdateEntityAsync()
        {
            try
            {
                if (EditValidateAll() ?? true)
                {
                    await OnUpdatingEntityAsync();

                    await CheckUpdatePolicyAsync();
                    var updateInput = MapToUpdateInput(EditingEntity);
                    await AppService.UpdateAsync(EditingEntityId, updateInput);

                    await OnUpdatedEntityAsync();
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        protected virtual bool? EditValidateAll()
        {
            throw new NotImplementedException();
        }

        protected virtual Task OnUpdatingEntityAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnUpdatedEntityAsync()
        {
            return Task.CompletedTask;

        }

        protected virtual string GetDeleteConfirmationMessage()
        {
            return UiLocalizer["ItemWillBeDeletedMessage"];
        }

        protected virtual async Task CheckCreatePolicyAsync()
        {
            await CheckPolicyAsync(CreatePolicyName);
        }

        protected virtual async Task CheckUpdatePolicyAsync()
        {
            await CheckPolicyAsync(UpdatePolicyName);
        }

        protected virtual async Task CheckDeletePolicyAsync()
        {
            await CheckPolicyAsync(DeletePolicyName);
        }

        /// <summary>
        /// Calls IAuthorizationService.CheckAsync for the given <paramref name="policyName"/>.
        /// Throws <see cref="AbpAuthorizationException"/> if given policy was not granted for the current user.
        ///
        /// Does nothing if <paramref name="policyName"/> is null or empty.
        /// </summary>
        /// <param name="policyName">A policy name to check</param>
        protected virtual async Task CheckPolicyAsync([CanBeNull] string policyName)
        {
            if (string.IsNullOrEmpty(policyName))
            {
                return;
            }

            await AuthorizationService.CheckAsync(policyName);
        }



        protected virtual ValueTask SetEntityActionsAsync()
        {
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetTableColumnsAsync()
        {
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            return ValueTask.CompletedTask;
        }


        protected virtual Task OnPermissionAsync(TGetListOutputDto item)
        {
            return Task.CompletedTask;
        }



        protected virtual Task CreateClose()
        {
            return Task.CompletedTask;
        }
        protected virtual Task EditClose()
        {
            return Task.CompletedTask;
        }

        public async Task DeleteAsync()
        {

        }
        public async Task<bool> ClickBeforeDelete(TGetListOutputDto model)
        {
            var result = false;
            try
            {
                await CheckDeletePolicyAsync();
                result = true;
            }
            catch (Exception ex)
            {
                await this.HandleErrorAsync(ex);
            }
            return result;
        }

    }



}
