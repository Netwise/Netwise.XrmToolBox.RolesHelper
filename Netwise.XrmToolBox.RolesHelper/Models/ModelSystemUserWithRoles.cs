using System.Collections.Generic;

namespace Netwise.XrmToolBox.RolesHelper.Models
{
    /// <summary>
    /// Model which contains data about SystemUser and they Roles.
    /// </summary>
    public class ModelSystemUserWithRoles
    {
        public ModelSystemUser SystemUser { get; set; }
        public List<ModelRole> Roles { get; set; }
    }
}