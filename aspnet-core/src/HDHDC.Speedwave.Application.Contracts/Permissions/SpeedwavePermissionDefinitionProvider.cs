using HDHDC.Speedwave.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HDHDC.Speedwave.Permissions
{
    public class SpeedwavePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(SpeedwavePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(SpeedwavePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SpeedwaveResource>(name);
        }
    }
}
