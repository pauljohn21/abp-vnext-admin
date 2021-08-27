using System;
using System.Threading.Tasks;
using BootstrapBlazor.Components;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.DependencyInjection;

namespace Tchivs.Abp.BootstrapUI
{
    [Dependency(ReplaceServices = true)]
    public class BootstrapUiMessageService : IUiMessageService, IScopedDependency
    {
        [Inject] public MessageService MessageService { get; set; }

        public Task Info(string message, string title = null, Action<UiMessageOptions> options = null)
        {
            return  Show(message,Color.Info);
        }

          Task Show(string message,Color color)
        {
            return MessageService.Show(new MessageOption()
            {
                Content = message, Color = color
            });
        }
        public Task Success(string message, string title = null, Action<UiMessageOptions> options = null)
        {
            
            return Show(message,Color.Success);
        }

        public Task Warn(string message, string title = null, Action<UiMessageOptions> options = null)
        {
            return Show(message,Color.Warning);
        }

        public Task Error(string message, string title = null, Action<UiMessageOptions> options = null)
        {
            return Show(message,Color.Danger);
        }

        public async Task<bool> Confirm(string message, string title = null, Action<UiMessageOptions> options = null)
        {
            return true;
        }
    }
}