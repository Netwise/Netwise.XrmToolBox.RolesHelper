using Microsoft.Xrm.Sdk;
using Netwise.XrmToolBox.RolesHelper.Models;
using OfficeOpenXml;
using System.Drawing;
using System.Linq;

namespace Netwise.XrmToolBox.RolesHelper.Exporters.Excels.Configurations
{
    public class ExcelUsersAndRolesConfiguration : AbstractExcelConfiguration
    {
        public override void PrepareData(ExcelPackage package, PluginControl pluginControl)
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Users with Roles");
            this.PrepareHeader(worksheet);

            // Get all users
            IOrderedEnumerable<Entity> users = this.GetAllUsers(pluginControl);
            // Number of row from which to start adding user-role pairs
            int rowNumber = 2;
            // For each user get roles using fetchxml
            foreach (Entity user in users)
            {
                EntityCollection roles = pluginControl.RetrieveMultiple(FetchQueriesHelper.GetRolesForUser(user.Id));
                // For each role insert row to Excel
                foreach (Entity role in roles.Entities)
                {
                    this.InsertRow(worksheet, user, role, rowNumber);
                    rowNumber++;
                }
            }

            this.SignExcel(package);
        }

        private void InsertRow(ExcelWorksheet worksheet, Entity user, Entity role, int rowNumber)
        {
            // Add User
            this.PrepareCells(worksheet, worksheet.Cells[rowNumber, 1], Color.Black, Color.Empty, user[ModelSystemUser.Fields.FullName]);

            // Add Role
            AliasedValue retrievedName = role[ModelRole.Fields.RetrievedName] as AliasedValue;
            this.PrepareCells(worksheet, worksheet.Cells[rowNumber, 2], Color.Black, Color.Empty, retrievedName.Value);
        }

        private void PrepareHeader(ExcelWorksheet worksheet)
        {
            this.PrepareCells(worksheet, worksheet.Cells["A1"], Color.White, NTW_BLUE, "User");
            this.PrepareCells(worksheet, worksheet.Cells["B1"], Color.White, NTW_GREEN, "Role");
        }

        private IOrderedEnumerable<Entity> GetAllUsers(PluginControl pluginControl)
        {
            EntityCollection users = pluginControl.RetrieveMultiple(FetchQueries.GetUsers);
            IOrderedEnumerable<Entity> orderedUsers = users.Entities.OrderBy(selector => selector.Attributes[ModelSystemUser.Fields.FullName]);
            return orderedUsers;
        }
    }
}