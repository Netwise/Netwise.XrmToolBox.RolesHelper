using Microsoft.Xrm.Sdk.Metadata;
using System;

namespace Netwise.XrmToolBox.RolesHelper.Models
{
    /// <summary>
    /// Privilege description.
    /// </summary>
    public class ModelPrivilege
    {
        public string Name { get; set; }
        public Guid PrivilegeId { get; set; }
        public PrivilegeType AccessRight { get; set; }

        public class Fields
        {
            public const string Name = "name";
            public const string PrivilegeId = "privilegeid";
            public const string AccessRight = "accessright";
        }
    }
}