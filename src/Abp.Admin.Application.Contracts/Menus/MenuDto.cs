using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Abp.Admin.Menus
{
   public class MenuDto : FullAuditedEntityDto<Guid>
    {
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        public virtual Collection<MenuDto> Menus { get; protected set; } //子集合
    }
    public class CreateUpdateMenuDto
    {
        public Guid? ParentId { get; set; }
        [StringLength(16)] 
        [Required] public string Name { get; set; }
        public string Url { get; set; }
        public int Sort { get; set; }
       // public virtual Collection<MenuDto> Menus { get; protected set; } //子集合
    }
    public interface IMenuAppService : ICrudAppService<MenuDto, Guid, PagedAndIncludeSortedResultRequestDto, CreateUpdateMenuDto, CreateUpdateMenuDto> { }
   
}
