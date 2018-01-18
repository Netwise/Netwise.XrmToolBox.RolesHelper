using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System;
using System.Drawing;

namespace Netwise.XrmToolBox.RolesHelper.Exporters.Excels.Configurations
{
    /// <summary>
    /// Contains methods for all configurations which exports data to Excel.
    /// </summary>
    public abstract class AbstractExcelConfiguration : IExporterConfiguration<ExcelPackage, PluginControl>
    {
        #region Constants

        /// <summary>
        /// Full plugin name.
        /// </summary>
        protected const string PLUGIN_NAME = "Netwise.XrmToolBox.RolesHelper";
        /// <summary>
        /// Number of rows reserved for headers.
        /// </summary>
        protected const int HEADER_ROWS = 2;
        /// <summary>
        /// Number of cells per row.
        /// </summary>
        protected const int CELLS_IN_ROW = 10;
        /// <summary>
        /// Default Excel column width.
        /// </summary>
        protected const double EXCEL_COLUMN_WIDTH = 8.43;
        /// <summary>
        /// Default Excel row height.
        /// </summary>
        protected const double EXCEL_ROW_HEIGHT = 15;
        /// <summary>
        /// Excel cell width in pixels.
        /// </summary>
        protected const int EXCEL_COLUMN_WIDTH_PIXELS = 64;
        /// <summary>
        /// Excel row height in pixels.
        /// </summary>
        protected const int EXCEL_ROW_HEIGHT_PIXELS = 20;
        /// <summary>
        /// Netwise Blue.
        /// </summary>
        protected static readonly Color NTW_BLUE = Color.FromArgb(47, 86, 131);
        /// <summary>
        /// Netwise Green.
        /// </summary>
        protected static readonly Color NTW_GREEN = Color.FromArgb(166, 206, 57);

        #endregion

        protected void SignExcel(ExcelPackage package)
        {
            // Sign Excel file
            package.Workbook.Properties.Author = PLUGIN_NAME;
            package.Workbook.Properties.LastModifiedBy = PLUGIN_NAME;
            package.Workbook.Properties.Created = DateTime.Now;
            /// Short description - in this order properties are shown in file properties
            package.Workbook.Properties.Title = $"Generated with: { PLUGIN_NAME }";
            package.Workbook.Properties.Subject = "https://github.com/Netwise/Netwise.XrmToolBox.RolesHelper";
        }

        protected void PrepareCells(ExcelWorksheet worksheet, ExcelRange cells, Color fontColor, Color backgroundColor, object value,
            ExcelHorizontalAlignment horizontalAlignment = ExcelHorizontalAlignment.Center,
            ExcelVerticalAlignment verticalAlignment = ExcelVerticalAlignment.Center,
            ExcelFillStyle patternType = ExcelFillStyle.Solid)
        {
            cells.Merge = true;
            cells.Style.VerticalAlignment = verticalAlignment;
            cells.Style.HorizontalAlignment = horizontalAlignment;
            cells.Style.Font.Bold = true;
            cells.Style.Font.Color.SetColor(fontColor);

            // Set background color only if specified - it will remove bounding box around cell
            if ((backgroundColor != null) && (backgroundColor != Color.Empty))
            {
                cells.Style.Fill.PatternType = patternType;
                cells.Style.Fill.BackgroundColor.SetColor(backgroundColor);
            }

            // Fill right cell
            if (value is string) // For string add text
            {
                cells.Value = value;
            }
            else if (value is System.Windows.Controls.Image) // For Image try to insert image - by default it will understand Image as System.Drawing.Image (it is not)
            {
                System.Windows.Controls.Image parsed = value as System.Windows.Controls.Image;
                Image image = parsed.ToDrawingImage();
                int rowIndex = cells.Start.Row;
                int columnIndex = cells.Start.Column;

                ExcelPicture picture = worksheet.Drawings.AddPicture($"Picture for cell [{ rowIndex };{ columnIndex }]", image);

                picture.SetPosition(
                    rowIndex - 1,
                    (int)(worksheet.Row(rowIndex).Height / 4), // row height center
                    columnIndex - 1,
                    (int)(worksheet.Column(columnIndex).Width * 3)); // column width center
            }
        }

        public abstract void PrepareData(ExcelPackage package, PluginControl pluginControl);
    }
}