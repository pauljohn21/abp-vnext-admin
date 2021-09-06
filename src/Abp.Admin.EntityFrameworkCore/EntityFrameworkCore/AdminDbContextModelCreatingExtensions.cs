using System;
using Abp.Admin.Menus;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;

namespace Abp.Admin.EntityFrameworkCore
{
    public static class AdminDbContextModelCreatingExtensions
    {
        public static void ConfigureAdmin(
          this ModelBuilder builder
          )
        {
            builder.Entity<Menu>(b =>
            {
                b.ToTable($"{AdminConsts.DbTablePrefix}{nameof(Menu)}{AdminConsts.DbSchema}");
                b.ConfigureByConvention();
                b.ConfigureFullAuditedAggregateRoot();
                b.Property(x => x.Name).IsRequired().HasMaxLength(AdminDbProperties.MaxMenuNameLength);
                b.Property(x => x.Url).HasMaxLength(AdminDbProperties.MaxMenuUrlLength);
                b.Property(x => x.Icon).HasMaxLength(AdminDbProperties.MaxMenuIconLength);
                b.Property(x => x.Type).HasDefaultValue(MenuType.Menu);
                b.Property(x => x.Sort).HasDefaultValue(0);
                b.HasMany<RoleMenu>().WithOne().HasForeignKey(x => x.MenuId).OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<IdentityRole>(b =>
            {
                b.ConfigureByConvention();
                b.ConfigureExtraProperties();
                b.HasMany<RoleMenu>().WithOne().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<RoleMenu>(b =>
            {
                b.ToTable($"{AdminConsts.DbTablePrefix}{nameof(RoleMenu)}{AdminConsts.DbSchema}");
                b.ConfigureByConvention();
                b.HasKey(nameof(RoleMenu.MenuId), nameof(RoleMenu.RoleId));
            });
        }
    }
}
