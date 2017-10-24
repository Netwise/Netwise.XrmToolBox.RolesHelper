using System;

namespace Netwise.XrmToolBox.RolesHelper.Models
{
    /// <summary>
    /// Role description.
    /// </summary>
    public class ModelRole
    {
        public string Name { get; set; }
        public Guid RoleId { get; set; }

        public class Fields
        {
            public const string Name = "name";
            public const string RoleId = "roleid";
            // Used when Role is retrieved
            public const string RetrievedName = "role2.name";
            public const string RetrievedRoleId = "role2.roleid";
        }
    }
}