using System;
using System.Collections.Generic;
using System.Linq;

namespace RPNL.Net.Utilities.PermissionUtil
{
    public static class PermissionPackers
    {
        public static string PackPermissionsIntoString(this IEnumerable<PermissionsEnum> permissions)
        {
            return permissions.Aggregate("", (s, permission) => s + (char)permission);
        }

        public static IEnumerable<PermissionsEnum> UnpackPermissionsFromString(this string packedPermissions)
        {
            if (packedPermissions == null)
                throw new ArgumentNullException(nameof(packedPermissions));
            foreach (var character in packedPermissions)
            {
                yield return ((PermissionsEnum)character);
            }
        }

        public static PermissionsEnum? FindPermissionViaName(this string permissionName)
        {
            return Enum.TryParse(permissionName, out PermissionsEnum permission)
                ? (PermissionsEnum?)permission
                : null;
        }
    }
}
