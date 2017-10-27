using System.Drawing;
using WB_Permissions;

namespace Netwise.XrmToolBox.RolesHelper.Models
{
    /// <summary>
    /// Depth Mask.
    /// </summary>
    public class ModelDepthMask
    {
        public PrivilegeDepthMaskEnum Value { get; private set; }
        public string Name { get; private set; }
        public Bitmap Image { get; private set; }
        public RoleRange RoleRange { get; set; }

        public ModelDepthMask(PrivilegeDepthMaskEnum value, string name, Bitmap image, RoleRange range)
        {
            this.Value = value;
            this.Name = name;
            this.Image = image;
            this.RoleRange = range;
        }
    }
}