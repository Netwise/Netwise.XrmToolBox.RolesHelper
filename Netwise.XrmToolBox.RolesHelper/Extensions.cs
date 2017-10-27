using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Netwise.XrmToolBox.RolesHelper.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Netwise.XrmToolBox.RolesHelper
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts current Access Right value to <see cref="ModelAccessRight"/>.
        /// </summary>
        public static ModelAccessRight ToAccessRight(this int accessRight)
        {
            return Dictionary.AccessRights.Where(model => (int)model.Value == accessRight).First();
        }

        /// <summary>
        /// Parse current <see cref="Entity"/> to <see cref="ModelSystemUser"/>.
        /// </summary>
        public static ModelSystemUser ToSystemUser(this Entity entity)
        {
            return new ModelSystemUser()
            {
                FullName = entity[ModelSystemUser.Fields.FullName].ToString(),
                SystemUserId = Guid.Parse(entity[ModelSystemUser.Fields.SystemUserId].ToString())
            };
        }

        /// <summary>
        /// Parse current <see cref="Entity"/> to <see cref="ModelPrivilege"/>.
        /// </summary>
        public static ModelPrivilege ToProvilege(this Entity entity)
        {
            return new ModelPrivilege()
            {
                PrivilegeId = Guid.Parse(entity[ModelPrivilege.Fields.PrivilegeId].ToString()),
                Name = entity[ModelPrivilege.Fields.Name].ToString(),
                AccessRight = Int32.Parse(entity[ModelPrivilege.Fields.AccessRight].ToString()).ToAccessRight().Value
            };
        }

        /// <summary>
        /// Parse current <see cref="Entity"/> to <see cref="ModelRole"/>.
        /// </summary>
        public static ModelRole ToRole(this Entity entity)
        {
            return new ModelRole()
            {
                RoleId = Guid.Parse(entity[ModelRole.Fields.RoleId].ToString()),
                Name = entity[ModelRole.Fields.Name].ToString()
            };
        }

        /// <summary>
        /// Parse current <see cref="Entity"/> to Retrieved<see cref="ModelRole"/>.
        /// </summary>
        public static ModelRole ToRetrievedRole(this Entity entity)
        {
            // FetchXml retrieves an Aliased Value for SQL Join
            var aliasedGuid = (AliasedValue)entity[ModelRole.Fields.RetrievedRoleId];
            var guidString = aliasedGuid.Value.ToString();
            // Aliased Role Name
            var aliasedName = (AliasedValue)entity[ModelRole.Fields.RetrievedName];
            var roleName = aliasedName.Value.ToString();
            // Return Role Model
            return new ModelRole()
            {
                RoleId = Guid.Parse(guidString),
                Name = roleName
            };
        }

        /// <summary>
        /// Parse current <see cref="Entity"/> to <see cref="ModelRolePrivilege"/>.
        /// </summary>
        public static ModelRolePrivilege ToRolePrivilege(this Entity entity)
        {
            return new ModelRolePrivilege()
            {
                RolePrivilegeId = Guid.Parse(entity[ModelRolePrivilege.Fields.RolePrivilegeId].ToString()),
                PrivilegeId = Guid.Parse(entity[ModelRolePrivilege.Fields.PrivilegeId].ToString()),
                RoleId = Guid.Parse(entity[ModelRolePrivilege.Fields.RoleId].ToString()),
                Mask = Int32.Parse(entity[ModelRolePrivilege.Fields.Mask].ToString())
            };
        }

        /// <summary>
        /// Parse current <see cref="EntityCollection"/> to <see cref="ModelSystemUserWithRoles"/>.
        /// </summary>
        public static ModelSystemUserWithRoles ToSystemModelWithRoles(this EntityCollection entities)
        {
            // Initialize new object
            var userWithRoles = new ModelSystemUserWithRoles()
            {
                SystemUser = null,
                Roles = new List<ModelRole>()
            };
            // Parse Roles and add them to List
            foreach (var entity in entities.Entities)
            {
                // Set User if null
                if (userWithRoles.SystemUser == null)
                {
                    userWithRoles.SystemUser = entity.ToSystemUser();
                }
                // Add Role to List
                var role = entity.ToRetrievedRole();
                userWithRoles.Roles.Add(role);
            }
            return userWithRoles;
        }

        /// <summary>
        /// Unchecked all Items on <see cref="CheckedListBox"/>.
        /// </summary>
        public static void ClearAllSelections(this CheckedListBox listBox)
        {
            for (int i = 0; i < listBox.Items.Count; ++i)
            {
                listBox.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        /// <summary>
        /// Returns index of given <see cref="ModelRole"/> based on Role's Name and <see cref="Guid"/>.
        /// </summary>
        public static int GetIndexOfRole(this List<ModelRole> list, ModelRole modelRole)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                var role = list[i];

                if (role.Name.Equals(modelRole.Name) &&
                    role.RoleId == modelRole.RoleId)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Converts current image to Base64 <see cref="string"/>. 
        /// By default images in Resources are in GIF format.
        /// </summary>
        public static string ToBase64String(this Bitmap image)
        {
            return ToBase64String(image, ImageFormat.Gif);
        }

        /// <summary>
        /// Converts current image to Base64 <see cref="string"/>. 
        /// </summary>
        public static string ToBase64String(this Bitmap image, ImageFormat imageFormat)
        {
            // To Stream Conversion
            using (var stream = new MemoryStream())
            {
                image.Save(stream, imageFormat);
                var streamBytes = stream.ToArray();
                return Convert.ToBase64String(streamBytes);
            }
        }

        /// <summary>
        /// Returns List of all <see cref="EntityMetadata"/>'s.
        /// </summary>
        public static List<EntityMetadata> GetEntityMetadatas(this IOrganizationService service)
        {
            // Prepare Request
            var request = new RetrieveAllEntitiesRequest()
            {
                EntityFilters = EntityFilters.Privileges,
                RetrieveAsIfPublished = true
            };
            // Execute Request
            var response = (RetrieveAllEntitiesResponse)service.Execute(request);
            // Return List of all Entity metadata
            // Where - there is a label which can be displayed (NOTE: System-specific Entities doesn't have Label or has empty string)
            // OrderBy - Entity Logical name
            return response.EntityMetadata
                .Where(model =>
                {
                    try
                    {
                        if (!model.DisplayName.UserLocalizedLabel.Label.Equals(""))
                        {
                            return true;
                        }
                        return false;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                })
                .OrderBy(entity => entity.LogicalName)
                .ToList();
        }
    }
}