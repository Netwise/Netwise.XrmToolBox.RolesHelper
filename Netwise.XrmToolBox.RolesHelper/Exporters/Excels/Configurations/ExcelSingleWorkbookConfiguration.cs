using OfficeOpenXml;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WB_Permissions;

namespace Netwise.XrmToolBox.RolesHelper.Exporters.Excels.Configurations
{
    public class ExcelSingleWorkbookConfiguration : AbstractExcelConfiguration
    {
        public override void PrepareData(ExcelPackage package, PluginControl dataHolder)
        {
            List<DataRowMetadata> allRows = dataHolder.wB_Permissions1.RowsMetadata;

            // Worksheet #1 - All
            this.PrepareWorksheet(package, "All Roles", allRows);

            // Worksheet #2...n - Worksheet per Role
            CheckedListBox.CheckedItemCollection checkedItems = dataHolder.GetCheckedRoles();
            string roleName = "";
            for (int i = 0; i < checkedItems.Count; ++i)
            {
                roleName = checkedItems[i].ToString();
                List<DataRowMetadata> rows = allRows.Where(row => row.DataRow.Role.Equals(roleName)).ToList();
                this.PrepareWorksheet(package, roleName, rows);
            }

            this.SignExcel(package);
        }

        private void PrepareWorksheet(ExcelPackage package, string worksheetName, List<DataRowMetadata> rows)
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(worksheetName);
            this.PrepareHeader(worksheet);
            this.PrepareDataRows(worksheet, rows);
        }

        private void PrepareDataRows(ExcelWorksheet worksheet, List<DataRowMetadata> rows)
        {
            for (int i = 0; i < rows.Count; ++i)
            {
                DataRowMetadata row = rows[i];
                DataRow dataRow = row.DataRow;
                this.PrepareSingleRow(worksheet, i + HEADER_ROWS + 1, Color.Black, Color.Empty, new object[]
                {
                    dataRow.EntityName,
                    dataRow.EntityLogicalName,
                    dataRow.Role,
                    row.ReadImage,
                    row.WriteImage,
                    row.DeleteImage,
                    row.AppendImage,
                    row.AppendToImage,
                    row.AssignImage,
                    row.ShareImage
                }, 15);
            }
        }

        private void PrepareHeader(ExcelWorksheet worksheet)
        {
            this.PrepareCells(worksheet, worksheet.Cells["A1:J1"], Color.White, NTW_BLUE, "Entity Permissions");

            this.PrepareSingleRow(worksheet, 2, Color.White, NTW_GREEN, new object[]
            {
                "Entity Name", "Entity Logical Name", "Role", "Read", "Write", "Delete", "Append", "Append To", "Assign", "Share"
            }, 20);
        }

        private void PrepareSingleRow(ExcelWorksheet worksheet, int row, Color fontColor, Color backgroundColor, object[] values,
            double columnWidth = EXCEL_COLUMN_WIDTH)
        {
            for (int i = 0; i < CELLS_IN_ROW; ++i)
            {
                worksheet.Column(i + 1).Width = columnWidth;
                this.PrepareCells(worksheet, worksheet.Cells[row, i + 1], fontColor, backgroundColor, values[i]);
            }

            // For 2 first columns (EntityName, EntityLogicalName) change size
            for (int i = 0; i < 2; ++i)
            {
                worksheet.Column(i + 1).Width = 2 * columnWidth;
            }
        }
    }
}