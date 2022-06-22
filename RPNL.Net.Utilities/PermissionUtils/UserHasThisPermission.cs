using System.Linq;
using System.Security.Claims;

namespace RPNL.Net.Utilities.PermissionUtils
{
    public static class UserHasThisPermission
    {
        /// <summary>
        /// This returns true if the current user has the permission
        /// </summary>
        /// <param name="user"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static bool AppUserHasThisPermission(this ClaimsPrincipal user, PermissionsEnum permission)
        {
            var permissionClaim =
                user?.Claims.SingleOrDefault(x => x.Type == PermissionConstants.PackedPermissionClaimType);
            return permissionClaim?.Value.UnpackPermissionsFromString().ToArray().UserHasThisPermission(permission) == true;
        }
    }
}
