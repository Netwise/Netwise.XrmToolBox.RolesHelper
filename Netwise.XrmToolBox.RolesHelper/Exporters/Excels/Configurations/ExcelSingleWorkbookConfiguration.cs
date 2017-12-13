using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Netwise.XrmToolBox.RolesHelper.Exporters.Excels.Configurations
{
    public class ExcelSingleWorkbookConfiguration : AbstractExporterConfiguration<ExcelPackage, PluginControl>
    {
        public override void PrepareData(ExcelPackage package, PluginControl dataHolder)
        {
            // Worksheet #1 - All
            ExcelWorksheet worksheetAll = package.Workbook.Worksheets.Add("All Roles"); // this[row, column]
            this.PrepareHeader(worksheetAll);

            // Worksheet #2...n - Worksheet per Role
        }

        private void PrepareHeader(ExcelWorksheet worksheet)
        {
            // Entity Permissions
            ExcelRange entityPermissionsRange = worksheet.Cells["A1:J1"];
            this.PrepareHeaderCells(entityPermissionsRange, Color.White, Color.FromArgb(47, 86, 131), "Entity Permissions");

            // Properties columns headers
            Color ntwColor = Color.FromArgb(166, 206, 57);
            this.PrepareHeaderCells(worksheet.Cells["A2"], Color.White, ntwColor, "Entity Name");
            this.PrepareHeaderCells(worksheet.Cells["B2"], Color.White, ntwColor, "Entity Logical Name");
            this.PrepareHeaderCells(worksheet.Cells["C2"], Color.White, ntwColor, "Role");
            this.PrepareHeaderCells(worksheet.Cells["D2"], Color.White, ntwColor, "Read");
            this.PrepareHeaderCells(worksheet.Cells["E2"], Color.White, ntwColor, "Write");
            this.PrepareHeaderCells(worksheet.Cells["F2"], Color.White, ntwColor, "Delete");
            this.PrepareHeaderCells(worksheet.Cells["G2"], Color.White, ntwColor, "Append");
            this.PrepareHeaderCells(worksheet.Cells["H2"], Color.White, ntwColor, "Append To");
            this.PrepareHeaderCells(worksheet.Cells["I2"], Color.White, ntwColor, "Assign");
            this.PrepareHeaderCells(worksheet.Cells["J2"], Color.White, ntwColor, "Share");
        }

        private void PrepareHeaderCells(ExcelRange cells, Color fontColor, Color backgroundColor, string text)
        {
            cells.Merge = true;
            cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cells.Style.Font.Bold = true;
            cells.Style.Font.Color.SetColor(fontColor);
            cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(backgroundColor);
            cells.Value = text;
        }
    }
}