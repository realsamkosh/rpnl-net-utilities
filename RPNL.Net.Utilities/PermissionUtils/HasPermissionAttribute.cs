using Microsoft.AspNetCore.Authorization;
using System;

namespace RPNL.Net.Utilities.PermissionUtil
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionsEnum permission) : base(permission.ToString())
        { }
    }
}
