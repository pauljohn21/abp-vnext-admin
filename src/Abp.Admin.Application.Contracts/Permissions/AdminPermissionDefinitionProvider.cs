using Abp.Admin.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.Admin.Permissions
{
    public class AdminPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var defaultGroup = context.AddGroup(AdminPermissions.GroupName);

            var menuPermission = defaultGroup.AddPermission(AdminPermissions.MenuPermissions.Default, L("Permission:Menu"));
            menuPermission.AddChild(AdminPermissions.MenuPermissions.Create, L("Permission:Create"));
            menuPermission.AddChild(AdminPermissions.MenuPermissions.Update, L("Permission:Update"));
            menuPermission.AddChild(AdminPermissions.MenuPermissions.Delete, L("Permission:Delete"));

            //Define your own permissions here. Example:
            //myGroup.AddPermission(AdminPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AdminResource>(name);
        }
    }
}
