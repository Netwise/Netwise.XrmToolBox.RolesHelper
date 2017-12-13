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

            // Entity Permissions title
            ExcelRange entityPermissionsRange = worksheetAll.Cells["A1:J1"];
            entityPermissionsRange.Merge = true;
            entityPermissionsRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            entityPermissionsRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            entityPermissionsRange.Style.Font.Bold = true;
            entityPermissionsRange.Style.Font.Color.SetColor(Color.White);
            entityPermissionsRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            entityPermissionsRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(47, 86, 131));
            entityPermissionsRange.Value = "Entity Permissions";

            // Worksheet #2...n - Worksheet per Role
        }
    }
}