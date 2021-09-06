using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
namespace Abp.Admin.Menus
{
    public class Menu : FullAuditedAggregateRoot<Guid>
    {
        public Guid? ParentId { get; protected set; }
        public string Name { get; protected set; }
        public string Url { get; set; }
        public bool Enable { get; set; }
        public int Sort { get; set; }
        public virtual Collection<Menu> Menus { get; protected set; } //子集合
        protected Menu()
        {

        }
        public Menu(Guid id, [NotNull] string name, string url, bool enable, int sort, Guid? pid)
        {
            this.Id = id;
            this.Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            this.Url = url;
            this.Enable = enable;
            this.ParentId = pid;
            this.Sort = sort;
            this.Menus = new Collection<Menu>();
        }
        public virtual void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name)); //验证
        }
        public virtual void AddChildrenMenu(Menu menu)
        {
            if (Menus.Any(x => x.Name == menu.Name))
            {//防止添加相同的菜单
                return;
            }
            menu.ParentId = this.Id;
            this.Menus.Add(menu);
        }
        public virtual void RemoveChildrenMenu(Guid id)
        {
            this.Menus.Remove(Menus.First(x => x.Id == id));
        }
    }
    /// <summary>
    /// 菜单与角色映射表
    /// </summary>
    public class RoleMenu : Entity
    {
        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
        protected RoleMenu() { }
        public RoleMenu(Guid roleId, Guid menuId) { this.RoleId = roleId; this.MenuId = menuId; }
        public override object[] GetKeys()
        {
            return new Object[] { RoleId, MenuId };
        }
    }
}
