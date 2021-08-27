using BootstrapBlazor.Components;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
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
        TUpdateInput> :
        Table<TGetListOutputDto>, ICrudBase<TAppService, TGetOutputDto,
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
        where TCreateInput : class, new()
        where TGetOutputDto : class, IEntityDto<TKey>, new()
        where TGetListOutputDto : class, IEntityDto<TKey>, new()
        where TGetListInput : class, new()
    {
        /// <summary>
        /// 设置创建工具栏方法
        /// </summary>
        [Parameter]
        public Func<FluentRenderTreeBuilder, Task> CreateToolbar { get; set; }

        /// <summary>
        /// 设置创建操作按钮方法
        /// </summary>
        [Parameter]
        public Func<FluentRenderTreeBuilder, Task> CreateRowButton { get; set; }

        /// <summary>
        /// 获得/设置 新建按钮回调方法
        /// </summary>
        [Parameter]
        public Func<Task<TCreateInput>> OnAddNewAsync { get; set; }

        /// <summary>
        /// 获得/设置 新建数据保存按钮异步回调方法
        /// </summary>
        [Parameter]
        public Func<TCreateInput, Task<bool>> OnSaveByAddAsync { get; set; }

        /// <summary>
        /// 获得/设置 修改数据保存按钮异步回调方法
        /// </summary>
        [Parameter]
        public Func<TUpdateInput, Task<bool>> OnSaveByEditAsync { get; set; }

        [Parameter] public RenderFragment<TCreateInput> CreateTemplate { get; set; }

        /// <summary>
        /// 获得/设置 EditModel 实例
        /// </summary>
        [NotNull]
        public new TCreateInput EditModel { get; set; } = new TCreateInput();

        [Inject] public TAppService AppService { get; set; }
        protected TGetListInput GetListInput { get; set; } = new TGetListInput();


        public AbpTable()
        {
            this.OnQueryAsync = QueryAsync;
            PageItemsSource = new int[] { 10, 20, 30, 50, 100 };
        }

        protected override async Task OnInitializedAsync()
        {
            OnDeleteAsync ??= DeleteAsync;
            OnSaveByAddAsync ??= SaveByAddAsync;
            await SetPermissionsAsync();
        }

        protected override Task OnParametersSetAsync()
        {
            TableToolbarTemplate = SetToolbarItems(TableToolbarTemplate);
           RowButtonTemplate = SetRowButtons(RowButtonTemplate);

            return base.OnParametersSetAsync();
        }


        protected virtual async Task<bool> SaveByAddAsync(TCreateInput input)
        {
            var res = false;
            try
            {
                await this.AppService.CreateAsync(input);
                res = true;
            }
            catch (Exception e)
            {
                await this.HandleErrorAsync(e);
            }

            return res;
        }

        /// <summary>
        /// 新建方法
        /// </summary>
        /// <returns></returns>
        protected new virtual async Task AddAsync()
        {
            try
            {
                await this.CheckCreatePolicyAsync();
                if (this.OnSaveByAddAsync != null)
                {
                    await ToggleLoading(true);
                    if (this.OnAddNewAsync != null)
                    {
                        this.EditModel = await OnAddNewAsync();
                    }
                    else
                    {
                        EditModel = new TCreateInput();
                    }

                    SelectedItems.Clear();
                    EditModalTitleString = AddModalTitle;
                    if (EditMode == EditMode.Popup)
                    {
                        if (DialogService != null)
                        {
                            await DialogService.ShowEditDialog(new EditDialogOption<TCreateInput>()
                            {
                                IsScrolling = ScrollingDialogContent,
                                ShowLoading = ShowLoading,
                                Title = EditModalTitleString,
                                Model = EditModel,
                                // Items = Columns.Where(i => i.Editable),
                                SaveButtonText = EditDialogSaveButtonText,
                                DialogBodyTemplate = CreateTemplate,
                                RowType = EditDialogRowType, Size = Size.None,
                                ItemsPerRow = EditDialogItemsPerRow,
                                LabelAlign = EditDialogLabelAlign,
                                // OnCloseAsync = async () => { },
                                OnSaveAsync = async context =>
                                {
                                    await ToggleLoading(true);
                                    var valid = await SaveByAddAsync((TCreateInput)context.Model);
                                    if (valid)
                                    {
                                        await QueryAsync();
                                    }

                                    await ToggleLoading(false);
                                    return valid;
                                }
                            });
                        }
                    }
                }
                else
                {
                    var option = new ToastOption
                    {
                        Category = ToastCategory.Error,
                        Title = AddButtonToastTitle,
                        Content = AddButtonToastContent
                    };
                    if (Toast != null)
                    {
                        await Toast.Show(option);
                    }
                }
            }
            catch (Exception e)
            {
                await this.HandleErrorAsync(e);
            }
            finally
            {
                await ToggleLoading(false);
                StateHasChanged();
            }
        }

        /// <summary>
        /// 删除数据方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected virtual async Task<bool> DeleteAsync(IEnumerable<TGetListOutputDto> input)
        {
            var result = false;
            try
            {
                await this.CheckDeletePolicyAsync();
                foreach (var item in input)
                {
                    await this.AppService.DeleteAsync(item.Id);
                }

                result = true;
            }
            catch (Exception e)
            {
                result = false;
                await this.HandleErrorAsync(e);
            }

            return result;
        }

        protected async virtual Task EditAsync(TGetListOutputDto item)

        {
        }

        RenderFragment SetToolbarItems(RenderFragment src)
        {
            var target = new RenderFragment(async builder =>
            {
                var fluentBuilder = builder.ToFluent();
                if (this.HasCreatePermission)
                {
                    //新建按钮放第一
                    fluentBuilder.OpenComponent(typeof(TableToolbarButton<TGetListOutputDto>))
                        .AddAttribute(nameof(Color), Color.Primary)
                        .AddAttribute(nameof(TableToolbarButton<TGetListOutputDto>.Icon), "fa fa-plus")
                        .AddAttribute(nameof(TableToolbarButton<TGetListOutputDto>.OnClick), AddAsync)
                        .AddAttribute(nameof(TableToolbarButton<TGetListOutputDto>.Text), this.AddButtonText)
                        .Close();
                }

                if (src != null)
                {
                    //合并razor的模板
                    builder.AddContent(0, src);
                }

                if (CreateToolbar != null)
                {
                    //通过代码创建
                    await this.CreateToolbar.Invoke(fluentBuilder);
                }

                if (this.HasDeletePermission)
                {
                    //删除按钮放最后
                    fluentBuilder.OpenComponent(typeof(TableToolbarPopconfirmButton<TGetListOutputDto>))
                        .AddAttribute(nameof(Color), Color.Danger)
                        .AddAttribute(nameof(TableToolbarPopconfirmButton<TGetListOutputDto>.Icon), "fa fa-remove")
                        .AddAttribute(nameof(TableToolbarPopconfirmButton<TGetListOutputDto>.Text),
                            this.DeleteButtonText)
                        .AddAttribute(nameof(TableToolbarPopconfirmButton<TGetListOutputDto>.CloseButtonText),
                            this.CancelDeleteButtonText)
                        .AddAttribute(nameof(TableToolbarPopconfirmButton<TGetListOutputDto>.Content),
                            this.ConfirmDeleteContentText)
                        .AddAttribute(nameof(TableToolbarPopconfirmButton<TGetListOutputDto>.ConfirmButtonText),
                            this.ConfirmDeleteButtonText)
                        .AddAttribute(nameof(TableToolbarPopconfirmButton<TGetListOutputDto>.ConfirmButtonColor),
                            Color.Danger)
                        .AddAttribute(nameof(TableToolbarPopconfirmButton<TGetListOutputDto>.OnConfirm),
                            this.DeleteAsync())
                        // .AddAttribute(nameof(TableToolbarPopconfirmButton<TGetListOutputDto>.OnBeforeClick), ConfirmDelete)
                        .Close();
                }
            });
            return target;
        }

        /// <summary>
        /// 设置每一行的操作按钮
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private RenderFragment<TGetListOutputDto> SetRowButtons(RenderFragment<TGetListOutputDto> src)
        {
            return (context =>
            {
                var target = new RenderFragment(async builder =>
                {
                    var fluentBuilder = builder.ToFluent();
                    if (this.HasUpdatePermission)
                    {
                        fluentBuilder.OpenComponent<Button>()
                            .AddAttribute(nameof(Size), Size.ExtraSmall)
                            .AddAttribute(nameof(Button.Icon), "fa fa-edit")
                            .AddAttribute(nameof(Button.Text), this.EditButtonText)
                            .AddAttribute(nameof(Button.OnClick),
                                EventCallback.Factory.Create<MouseEventArgs>(this, () => ClickEditButton(context)))
                            .Close();
                    }

                    if (src != null)
                    {
                        //合并razor的模板
                        builder.AddContent(0, src.Invoke(context));
                    }

                    if (CreateRowButton != null)
                    {
                        //通过代码创建
                        await this.CreateRowButton.Invoke(fluentBuilder);
                    }

                    if (HasDeletePermission)
                    {
                        fluentBuilder.OpenComponent<PopConfirmButton>()
                            .AddAttribute(nameof(Color), Color.Danger)
                            .AddAttribute(nameof(Size), Size.ExtraSmall)
                            .AddAttribute(nameof(PopConfirmButton.Icon), "fa fa-remove")
                            .AddAttribute(nameof(PopConfirmButton.Text), this.DeleteButtonText)
                            .AddAttribute(nameof(PopConfirmButton.CloseButtonText), this.CancelDeleteButtonText)
                            .AddAttribute(nameof(PopConfirmButton.Content), this.ConfirmDeleteContentText)
                            .AddAttribute(nameof(PopConfirmButton.ConfirmButtonText), this.ConfirmDeleteButtonText)
                            .AddAttribute(nameof(PopConfirmButton.OnBeforeClick), ClickBeforeDelete(context))
                            .AddAttribute(nameof(PopConfirmButton.OnConfirm), this.DeleteAsync())
                            .AddAttribute(nameof(Placement), Placement.Left)
                            .Close();
                    }
                });
                return target;
            });
        }

        private async Task ClickEditButton(TGetListOutputDto item)
        {
            SelectedItems.Clear();
            SelectedItems.Add(item);

            // 更新行选中状态
            await EditAsync(item);
        }


        protected virtual Task UpdateGetListInputAsync(QueryPageOptions options)
        {
            if (GetListInput is ISortedResultRequest sortedResultRequestInput)
            {
                sortedResultRequestInput.Sorting = options.SortName;
            }

            if (GetListInput is IPagedResultRequest pagedResultRequestInput)
            {
                pagedResultRequestInput.SkipCount = (options.PageIndex - 1) * options.PageItems;
            }

            if (GetListInput is ILimitedResultRequest limitedResultRequestInput)
            {
                limitedResultRequestInput.MaxResultCount = options.PageItems;
            }

            return Task.CompletedTask;
        }

        protected virtual async Task<QueryData<TGetListOutputDto>> QueryAsync(QueryPageOptions option)
        {
            try
            {
                await UpdateGetListInputAsync(option);
                bool isSearch = option.SearchText != null;
                var result = await this.AppService.GetListAsync(this.GetListInput);
                // var entities = MapToListViewModel(result.Items);
                return new QueryData<TGetListOutputDto>()
                {
                    Items = result.Items,
                    TotalCount = result.TotalCount,
                    IsSearch = isSearch
                };
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }

            return null;
        }

        protected virtual async Task HandleErrorAsync(Exception ex)
        {
            var option = new ToastOption
            {
                Category = ToastCategory.Error,
                Title = "错误",
                Content = ex.Message
            };
            if (Toast != null)
            {
                await Toast.Show(option);
            }
        }
    }
}