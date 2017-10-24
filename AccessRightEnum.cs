namespace Netwise.XrmToolBox.RolesHelper
{
    /// <summary>
    /// Enum which describes Access Right.
    /// </summary>
    public enum AccessRightEnum
    {
        Create = 32,
        Read = 1,
        Write = 2,
        Delete = 65536,
        Append = 4,
        AppendTo = 16,
        Assign = 524288,
        Share = 262144
    }
}