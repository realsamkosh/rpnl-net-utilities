using System;
using System.ComponentModel;
using System.Linq;

namespace RPNL.Net.Utilities.PermissionUtil
{
    public static class PermissionChecker
    {
        /// <summary>
        /// This is used by the policy provider to check the permission name string
        /// </summary>
        /// <param name="packedPermissions"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public static bool ThisPermissionIsAllowed(this string packedPermissions, string permissionName)
        {
            var usersPermissions = packedPermissions.UnpackPermissionsFromString().ToArray();

            if (!Enum.TryParse(permissionName, true, out PermissionsEnum permissionToCheck))
                throw new InvalidEnumArgumentException($"{permissionName} could not be converted to a {nameof(PermissionsEnum)}.");

            return usersPermissions.UserHasThisPermission(permissionToCheck);
        }

        /// <summary>
        /// This is the main checker of whether a user permissions allows them to access something with the given permission
        /// </summary>
        /// <param name="usersPermissions"></param>
        /// <param name="permissionToCheck"></param>
        /// <returns></returns>
        public static bool UserHasThisPermission(this PermissionsEnum[] usersPermissions, PermissionsEnum permissionToCheck)
        {
            return usersPermissions.Contains(permissionToCheck) || usersPermissions.Contains(PermissionsEnum.AccessAll);
        }
    }
}
