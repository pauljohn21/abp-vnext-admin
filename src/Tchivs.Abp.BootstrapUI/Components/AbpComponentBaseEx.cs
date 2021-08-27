using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components;

namespace Tchivs.Abp.BootstrapUI.Components
{
 public abstract  class AbpComponentBaseEx:AbpComponentBase
    {
        /// <summary>
        /// ToastService 服务实例
        /// </summary>
        [Inject]
        [NotNull]
        protected ToastService Toast { get; set; }
        /// <summary>
        /// DialogService 服务实例
        /// </summary>
        [Inject]
        [NotNull]
        protected DialogService DialogService { get; set; }
        protected AbpComponentBaseEx()
        {
        }
      
    }
}
