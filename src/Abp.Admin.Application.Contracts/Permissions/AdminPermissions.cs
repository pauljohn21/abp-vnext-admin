using Volo.Abp.Reflection;

namespace Abp.Admin.Permissions
{
    public static class AdminPermissions
    {
        public const string GroupName = "Admin";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";
        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(AdminPermissions));
        }
        public static class MenuPermissions
        {
            public const string Default = GroupName + ".Menu";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}