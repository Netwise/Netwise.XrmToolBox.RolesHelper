using System;

namespace Netwise.XrmToolBox.RolesHelper.Models
{
    /// <summary>
    /// RolePrivilege description.
    /// </summary>
    public class ModelRolePrivilege
    {
        public Guid RolePrivilegeId { get; set; }
        public Guid RoleId { get; set; }
        public Guid PrivilegeId { get; set; }
        public int Mask { get; set; }

        public class Fields
        {
            public const string RolePrivilegeId = "roleprivilegeid";
            public const string RoleId = "roleid";
            public const string PrivilegeId = "privilegeid";
            public const string Mask = "privilegedepthmask";
        }
    }
}