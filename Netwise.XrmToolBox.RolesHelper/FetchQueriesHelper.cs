using Netwise.XrmToolBox.RolesHelper.Models;
using System;

namespace Netwise.XrmToolBox.RolesHelper
{
    /// <summary>
    /// Various methods to help with FetchQueries.
    /// </summary>
    public static class FetchQueriesHelper
    {
        /// <summary>
        /// Returns actual Fetch XML <see cref="string"/> for <see cref="FetchQueries.GetRolesForUser"/>.
        /// </summary>
        public static string GetRolesForUser(ModelSystemUser user)
        {
            return string.Format(FetchQueries.GetRolesForUser, user.SystemUserId);
        }

        /// <summary>
        /// Returns actual Fetch XML <see cref="string"/> for <see cref="FetchQueries.GetPrivilegesForRole"/>.
        /// </summary>
        public static string GetPrivilegesForRole(ModelRole role)
        {
            return string.Format(FetchQueries.GetPrivilegesForRole, role.RoleId);
        }

        /// <summary>
        /// Returns actual Fetch XML <see cref="string"/> for <see cref="FetchQueries.GetPrivilegeById"/>.
        /// </summary>
        public static string GetPrivilegeById(Guid privilegeId)
        {
            return string.Format(FetchQueries.GetPrivilegeById, privilegeId);
        }
    }
}