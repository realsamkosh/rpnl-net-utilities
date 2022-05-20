using System;
using System.ComponentModel.DataAnnotations;

namespace RPNL.Net.Utilities.PermissionUtil
{
    public enum PermissionsEnum : short
    {
        NotSet = 0, //error condition

        [Display(GroupName = "User", Name = "Tenant Users", Description = "Can Act As Tenant Users")]
        CanActAsTenantUsers,
        [Display(GroupName = "Administrator", Name = "Tenant Administrator", Description = "Can Act As Tenant Administrator")]
        CanActAsTenantAdministrator,
        [Display(GroupName = "SuperAdmin", Name = "AccessAll", Description = "This allows the user to access every feature")]
        AccessAll = Int16.MaxValue,
    }
}
