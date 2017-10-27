using Microsoft.Xrm.Sdk.Metadata;
using Netwise.XrmToolBox.RolesHelper.Models;
using Netwise.XrmToolBox.RolesHelper.Properties;
using System.Collections.Generic;
using WB_Permissions;

namespace Netwise.XrmToolBox.RolesHelper
{
    /// <summary>
    /// Static Dictionary which holds constant data.
    /// </summary>
    public static class Dictionary
    {
        /// <summary>
        /// List which holds data about all Depth Masks.
        /// </summary>
        public static readonly List<ModelDepthMask> DepthMasks = new List<ModelDepthMask>()
        {
            new ModelDepthMask(PrivilegeDepthMaskEnum.None, "None", Resources.Empty, RoleRange.None),
            new ModelDepthMask(PrivilegeDepthMaskEnum.User, "User (Basic)", Resources.User, RoleRange.User),
            new ModelDepthMask(PrivilegeDepthMaskEnum.BusinessUnit, "Business Unit (Local)", Resources.BusinessUnit, RoleRange.BusinessUnit),
            new ModelDepthMask(PrivilegeDepthMaskEnum.ParentBusinessUnit, "Parental (Deep)", Resources.ParentBusinessUnit, RoleRange.ParentBusinessUnit),
            new ModelDepthMask(PrivilegeDepthMaskEnum.Organization, "Organization (Global)", Resources.Organization, RoleRange.Organization)
        };

        /// <summary>
        /// Returns an Empty Depth Mask.
        /// </summary>
        public static readonly ModelDepthMask DepthMaskEmpty = DepthMasks[0];

        /// <summary>
        /// List which holds data bout all Access Rights.
        /// </summary>
        public static readonly List<ModelAccessRight> AccessRights = new List<ModelAccessRight>()
        {
            //new ModelAccessRight(AccessRightEnum.Create, "Create"),
            //new ModelAccessRight(AccessRightEnum.Read, "Read"),
            //new ModelAccessRight(AccessRightEnum.Write, "Write"),
            //new ModelAccessRight(AccessRightEnum.Delete, "Delete"),
            //new ModelAccessRight(AccessRightEnum.Append, "Append"),
            //new ModelAccessRight(AccessRightEnum.AppendTo, "AppendTo"),
            //new ModelAccessRight(AccessRightEnum.Assign, "Assign"),
            //new ModelAccessRight(AccessRightEnum.Share, "Share")

            new ModelAccessRight(PrivilegeType.Create, "Create"),
            new ModelAccessRight(PrivilegeType.Read, "Read"),
            new ModelAccessRight(PrivilegeType.Write, "Write"),
            new ModelAccessRight(PrivilegeType.Delete, "Delete"),
            new ModelAccessRight(PrivilegeType.Append, "Append"),
            new ModelAccessRight(PrivilegeType.AppendTo, "AppendTo"),
            new ModelAccessRight(PrivilegeType.Assign, "Assign"),
            new ModelAccessRight(PrivilegeType.Share, "Share")
        };
    }
}