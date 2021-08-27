using BootstrapBlazor.Components;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tchivs.Abp.BootstrapUI.Components
{
    public partial class AbpTable<TAppService, TGetOutputDto,
               TGetListOutputDto,
               TKey,
               TGetListInput,
               TCreateInput,
               TUpdateInput>
    {
        #region Permission
        /// <summary>
        /// 验证服务
        /// </summary>
        [Inject] protected IAuthorizationService AuthorizationService { get; set; }
        public bool HasCreatePermission { get; set; }

        public bool HasUpdatePermission { get; set; }

        public bool HasDeletePermission { get; set; }
        /// <summary>
        /// 创建 权限名称
        /// </summary>
        [Parameter] public string CreatePolicyName { get; set; }
        /// <summary>
        /// 修改 权限名称
        /// </summary>
        [Parameter] public string UpdatePolicyName { get; set; }
        /// <summary>
        /// 删除 权限名称
        /// </summary>
        [Parameter] public string DeletePolicyName { get; set; }
        /// <summary>
        /// 判断是否有权限
        /// </summary>
        /// <param name="policyName">权限名称</param>
        /// <returns></returns>
        protected virtual async Task CheckPolicyAsync([CanBeNull] string policyName)
        {
            if (string.IsNullOrEmpty(policyName))
            {
                return;
            }

            await AuthorizationService.CheckAsync(policyName);
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
     
        
       
        protected virtual async Task SetPermissionsAsync()
        {
            if (!string.IsNullOrEmpty(CreatePolicyName)  )
            {
                HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
                this.ShowAddButton = HasCreatePermission;
            }

            if (!string.IsNullOrEmpty(UpdatePolicyName))
            {
                HasUpdatePermission = await AuthorizationService.IsGrantedAsync(UpdatePolicyName);
                this.ShowEditButton = HasUpdatePermission;
            }

            if (!string.IsNullOrEmpty(DeletePolicyName ))
            {
                HasDeletePermission = await AuthorizationService.IsGrantedAsync(DeletePolicyName);
                this.ShowDeleteButton = HasDeletePermission;
            }
        }

        #endregion

    }
}
