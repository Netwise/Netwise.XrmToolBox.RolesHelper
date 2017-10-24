using Microsoft.Xrm.Sdk.Metadata;
using Netwise.XrmToolBox.RolesHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Netwise.XrmToolBox.RolesHelper
{
    /// <summary>
    /// Class used for creating Initial HTML Web Page.
    /// </summary>
    public static class HtmlHelper
    {
        /// <summary>
        /// Dictionary which holds downloaded Privileges for each Role.
        /// </summary>
        public static Dictionary<ModelRole, List<ModelRolePrivilege>> LoadedPrivileges = new Dictionary<ModelRole, List<ModelRolePrivilege>>();
        /// <summary>
        /// List of all Sortable Column Headers.
        /// </summary>
        public static List<ModelSortableColumnHeader> SortableColumnHeaders = new List<ModelSortableColumnHeader>();
        /// <summary>
        /// Name / Id of the Main Table
        /// </summary>
        public static readonly string MainTableName = "MainTable";

        /// <summary>
        /// Main method used for creating Initial HTML WebPage.
        /// Returns created HTML WebPage.
        /// </summary>
        public static string PrepareHtml(ModelSystemUserWithRoles parsedUserRoles, PluginControl pluginControl, List<EntityMetadata> metadata)
        {
            // Clear Dictionary
            LoadedPrivileges.Clear();

            // StringBuilder used for HTML WebPage
            var stringBuilder = new StringBuilder();
            // Prepare HTML
            PrepareHtml(stringBuilder, parsedUserRoles, pluginControl, metadata);
            // Return HTML String
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Holds data about <see cref="StringBuilder"/>.
        /// </summary>
        private static void PrepareHtml(StringBuilder stringBuilder, ModelSystemUserWithRoles parsedUserRoles, PluginControl pluginControl, List<EntityMetadata> metadata)
        {
            // Html
            stringBuilder.AppendLine("<html>");
            {
                // Head
                PrepareHtmlHead(stringBuilder, parsedUserRoles, pluginControl, metadata);
                // Body
                PrepareHtmlBody(stringBuilder, parsedUserRoles, pluginControl, metadata);
            }
            stringBuilder.AppendLine("</html>");
        }

        /// <summary>
        /// Prepare WebPage's Head.
        /// </summary>
        private static void PrepareHtmlHead(StringBuilder sb, ModelSystemUserWithRoles parsedUserRoles, PluginControl pluginControl, List<EntityMetadata> metadata)
        {
            // Head
            sb.AppendLine("<head>");
            {
                // Inside Head
                // Add style
                sb.AppendLine("<style type='text/css'>");
                {
                    // CSS Elements for Images
                    Dictionary.DepthMasks.ForEach(model =>
                    {
                        sb.AppendLine($"img.{ model.Value } {{");
                        sb.AppendLine($"background:url(data:image/gif;base64,");
                        sb.AppendLine($"{ model.Image.ToBase64String() }");
                        sb.AppendLine($");");
                        sb.AppendLine("no-repeat left center;");
                        sb.AppendLine("width: 16px;");
                        sb.AppendLine("height: 16px;");
                        sb.AppendLine("}");
                    });
                    // CSS Element for Body
                    sb.AppendLine(".bd { padding:0x; margin:0px; font-size:10px }");
                    // CSS Element for Agenda Table
                    sb.AppendLine(".agendaTable { width=98%; font-size:11px; font-family:arial; padding:2px }");
                    // CSS Element for Agenta Table Header
                    sb.AppendLine(".agendaTableHeader { background-color:#2f5683; color:white; font-size:13px; text-align:center; padding:2px; margin:2px }");
                    // CSS Element for Table Column Header
                    sb.AppendLine(".columnHeader { background-color:#a6ce39; color:white; text-align:left; padding:2px; margin:2px }");

                    sb.AppendLine(".textCenter { text-align:center }");

                    // CSS Elements for Table
                    sb.AppendLine("td.wbc { border-bottom:1px solid #d3d3d3; text-align:center;  }");
                    sb.AppendLine("td.wbl { border-bottom:1px solid #d3d3d3; text-align:left; }");
                    sb.AppendLine("tr.hr { background-color:#41a9f4; height=1px; }");

                    // Entity Table Borders
                    sb.AppendLine(".borderBottom { border-bottom: 1px solid black }");

                    // Sorting Icon CSS
                    sb.AppendLine(".sortIcon { height: 12px; width: 12px; }");
                }
                sb.AppendLine("</style>");
            }
            sb.AppendLine("</head>");
        }

        /// <summary>
        /// Prepare WebPage's Body.
        /// </summary>
        private static void PrepareHtmlBody(StringBuilder sb, ModelSystemUserWithRoles user, PluginControl pluginControl, List<EntityMetadata> metadata)
        {
            // Body
            sb.AppendLine("<body class=\"bd\">");
            {
                // Inside Body

                /*
                 * Agenda Table is currently disabled
                 * 
                 * 
                // Build Privileges Agenda Table
                sb.AppendLine("<br/><table cellpadding='0' cellspacing='2' class=\"agendaTable\">");
                {
                    // Build Agenda Table Header
                    sb.AppendLine("<tr class=\"agendaTableHeader\">");
                    {
                        // Add Headers Names
                        sb.AppendLine("<th><font color=\"white\">Permission Name</font></th>");
                        sb.AppendLine("<th><font color=\"white\">Permission Icon</font></th>");
                    }
                    sb.AppendLine("</tr>");

                    // Add Permissions to Agenda Table
                    foreach (var permission in Dictionary.DepthMasks)
                    {
                        // Build Row for each Permission
                        sb.AppendLine("<tr>");
                        {
                            // Permission Name
                            sb.AppendLine("<td>");
                            sb.AppendLine(permission.Name);
                            sb.AppendLine("</td>");

                            // Permission Icon
                            sb.AppendLine("<td>");
                            sb.AppendLine($"<img class={ permission.Value } />");
                            sb.AppendLine("</td>");
                        }
                        sb.AppendLine("</tr>");
                    }
                }
                sb.AppendLine("</table>");

                // Add space between Tables
                sb.AppendLine("<br/>");
                sb.AppendLine("<br/>");
                sb.AppendLine("<br/>");
                sb.AppendLine("<br/>");
                */

                // Build Entity Table
                sb.AppendLine($"<table cellpadding='0' cellspacing='2' class=\"agendaTable\" id=\"{ MainTableName }\">");
                {
                    // Build Table Header
                    sb.AppendLine("<tr class=\"agendaTableHeader\">");
                    {
                        // +2 for Entity Name and Logical Name
                        // +1 for Roles
                        sb.AppendLine($"<th colspan={ Dictionary.AccessRights.Capacity + 2 + 1 }><font color=\"white\">Entity Permissions</font></th>");
                    }
                    sb.AppendLine("</tr>");

                    // Build Table Column Headers
                    sb.AppendLine("<tr class=\"columnHeader\">");
                    {
                        sb.AppendLine(ModelSortableColumnHeader.New("Entity Name", 0, entity => entity.DisplayName.UserLocalizedLabel.Label, pluginControl).ToHtmlElement());
                        sb.AppendLine(ModelSortableColumnHeader.New("Entity Logical Name", 1, entity => entity.LogicalName, pluginControl).ToHtmlElement());
                        sb.AppendLine("<th>Role</th>");
                        // Add Access Rights Headers
                        foreach (var accessRight in Dictionary.AccessRights)
                        {
                            sb.AppendLine($"<th class=\"textCenter\">{ accessRight.Name }</th>");
                        }
                    }
                    sb.AppendLine("</tr>");

                    // Download Roles
                    foreach (var role in user.Roles)
                    {
                        // Current Role Privileges
                        var rolePrivileges = pluginControl.GetRolePrivileges(role);
                        // Add to Dictionary
                        LoadedPrivileges.Add(role, rolePrivileges);
                    }

                    // Sort Entity Metadata if sorting option is set (by default sorting field is empty)
                    if (!pluginControl.CB_Sort.Text.Equals(string.Empty))
                    {
                        var selectedSortable = pluginControl.GetCurrentSelectedSortable();
                        metadata = metadata.OrderBy(selectedSortable.OrderBy).ToList();
                    }

                    // Prepare color nodes

                    // For each Entity
                    foreach (var entity in metadata)
                    {
                        // Main row for Entity
                        sb.AppendLine("<tr>");
                        {
                            // If User has any role
                            if (user.Roles.Count > 0)
                            {
                                // Label is after filtering so it should always be != null
                                sb.AppendLine($"<td rowspan='{ user.Roles.Count }' class=\"borderBottom\"><b>{ entity.DisplayName.UserLocalizedLabel.Label }</b></td>");
                                sb.AppendLine($"<td rowspan='{ user.Roles.Count }' class=\"borderBottom\">{ entity.LogicalName }</td>");
                                // Since cell summing is weird in HTML we need to build first 
                                BuildNodeForPrivilege(sb, user, entity, LoadedPrivileges.First());

                                // Build Privileges for each next Role in separate tr inside main tr
                                for (int i = 1; i < LoadedPrivileges.Count; ++i)
                                {
                                    // Row for current Privilege
                                    sb.AppendLine("<tr>");
                                    {
                                        BuildNodeForPrivilege(sb, user, entity, LoadedPrivileges.ElementAt(i));
                                    }
                                    sb.AppendLine("</tr>");
                                }
                            }
                        }
                        sb.AppendLine("</tr>");
                    }
                }
                sb.AppendLine("</table><br><br>");

                // Add additional Scripts
                sb.AppendLine("<script>");
                {
                    AddAdditionalScripts(sb, user, pluginControl, metadata);
                }
                sb.AppendLine("</script>");
            }
            sb.AppendLine("</body>");
        }

        /// <summary>
        /// Used for adding additional functions to main script.
        /// </summary>
        private static void AddAdditionalScripts(StringBuilder sb, ModelSystemUserWithRoles user, PluginControl pluginControl, List<EntityMetadata> metadata)
        {
        }

        /// <summary>
        /// Builds single tr.
        /// </summary>
        private static void BuildNodeForPrivilege(StringBuilder sb, ModelSystemUserWithRoles user, EntityMetadata entity, KeyValuePair<ModelRole, List<ModelRolePrivilege>> loadedPrivilege)
        {
            // Single Role line
            sb.AppendLine($"<td class=\"borderBottom\">{ loadedPrivilege.Key.Name }</td>");
            {
                // Add td for each Privilage
                // Find if this role has a specified pirvilege, if so add it; otherwise set empty
                foreach (var accessRight in Dictionary.AccessRights)
                {
                    // Privileges of current Entity
                    var entityPrivileges = entity.Privileges;
                    // Add color node Image to HTML
                    AddAccessRightNode(sb, entityPrivileges, loadedPrivilege.Value, accessRight);
                }
            }
        }

        /// <summary>
        /// Add Privilege color node to StringBuilder.
        /// </summary>
        private static void AddAccessRightNode(StringBuilder sb, SecurityPrivilegeMetadata[] entityPrivileges, List<ModelRolePrivilege> rolePrivileges, ModelAccessRight accessRight)
        {
            // Current Privilege for current Access Right
            SecurityPrivilegeMetadata currentEntityPrivilege = null;
            try
            {
                currentEntityPrivilege = entityPrivileges.Where(meta => meta.PrivilegeType == accessRight.Value).First();
            }
            catch (Exception)
            {
                sb.AppendLine($"<td align=\"center\" class=\"borderBottom\"><img class={ Dictionary.DepthMaskEmpty.Value } /></td>");
                return;
            }

            // RolePrivilege for current Entity in current Role
            ModelRolePrivilege currentRolePrivilege = null;
            try
            {
                currentRolePrivilege = rolePrivileges.Where(model => model.PrivilegeId == currentEntityPrivilege.PrivilegeId).First();
            }
            catch (Exception)
            {
                sb.AppendLine($"<td align=\"center\" class=\"borderBottom\"><img class={ Dictionary.DepthMaskEmpty.Value } /></td>");
                return;
            }

            // Current Depth Mask
            var depthMask = currentRolePrivilege.Mask.ToDepthMask();

            // Append node image
            sb.AppendLine($"<td align=\"center\" class=\"borderBottom\"><img class={ depthMask.Value } /></td>");
        }
    }
}