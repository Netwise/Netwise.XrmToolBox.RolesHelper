namespace Netwise.XrmToolBox.RolesHelper
{
    /// <summary>
    /// Enum Which describes Privilege's Depth Mask
    /// </summary>
    public enum PrivilegeDepthMaskEnum
    {
        None = 0,
        User = 1,
        BusinessUnit = 2,
        ParentBusinessUnit = 4,
        Organization = 8
    }
}