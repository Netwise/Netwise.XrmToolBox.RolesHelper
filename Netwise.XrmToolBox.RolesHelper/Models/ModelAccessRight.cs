using Microsoft.Xrm.Sdk.Metadata;

namespace Netwise.XrmToolBox.RolesHelper.Models
{
    /// <summary>
    /// Access Right.
    /// </summary>
    public class ModelAccessRight
    {
        public PrivilegeType Value { get; private set; }
        public string Name { get; private set; }

        public ModelAccessRight(PrivilegeType value, string name)
        {
            this.Value = value;
            this.Name = name;
        }
    }
}