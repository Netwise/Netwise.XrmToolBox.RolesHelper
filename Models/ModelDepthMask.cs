using System.Drawing;

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

        public ModelDepthMask(PrivilegeDepthMaskEnum value, string name, Bitmap image)
        {
            this.Value = value;
            this.Name = name;
            this.Image = image;
        }
    }
}