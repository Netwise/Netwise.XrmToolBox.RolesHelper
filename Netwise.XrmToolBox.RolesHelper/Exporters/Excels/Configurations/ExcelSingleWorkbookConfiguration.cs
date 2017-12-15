using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using WB_Permissions;

namespace Netwise.XrmToolBox.RolesHelper.Exporters.Excels.Configurations
{
    public class ExcelSingleWorkbookConfiguration : AbstractExporterConfiguration<ExcelPackage, PluginControl>
    {
        #region Constants

        /// <summary>
        /// Number of rows reserved for headers.
        /// </summary>
        private const int HEADER_ROWS = 2;
        /// <summary>
        /// Number of cells per row.
        /// </summary>
        private const int CELLS_IN_ROW = 10;
        /// <summary>
        /// Default Excel column width.
        /// </summary>
        private const double EXCEL_COLUMN_WIDTH = 8.43;
        /// <summary>
        /// Default Excel row height.
        /// </summary>
        private const double EXCEL_ROW_HEIGHT = 15;

        #endregion

        public override void PrepareData(ExcelPackage package, PluginControl dataHolder)
        {
            // Worksheet #1 - All
            ExcelWorksheet worksheetAll = package.Workbook.Worksheets.Add("All Roles");
            this.PrepareHeader(worksheetAll);
            this.PrepareDataRows(worksheetAll, dataHolder.wB_Permissions1.RowsMetadata);

            // Worksheet #2...n - Worksheet per Role
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
            this.PrepareCells(worksheet, worksheet.Cells["A1:J1"], Color.White, Color.FromArgb(47, 86, 131), "Entity Permissions");

            Color ntwColor = Color.FromArgb(166, 206, 57);
            this.PrepareSingleRow(worksheet, 2, Color.White, ntwColor, new object[]
            {
                "Entity Name", "Entity Logical Name", "Role", "Read", "Write", "Delete", "Append", "Append To", "Assign", "Share"
            }, 20);
        }

        private void PrepareSingleRow(ExcelWorksheet worksheet, int row, Color fontColor, Color backgroundColor, object[] values,
            double columnWidth = EXCEL_COLUMN_WIDTH)
        {
            for (int i = 0; i < CELLS_IN_ROW; ++i)
            {
                this.PrepareCells(worksheet, worksheet.Cells[row, i + 1], fontColor, backgroundColor, values[i]);
                worksheet.Column(i + 1).Width = columnWidth;
            }

            // For 2 first columns change size
            for (int i = 0; i < 2; ++i)
            {
                worksheet.Column(i + 1).Width = 2 * columnWidth;
            }
        }

        private void PrepareCells(ExcelWorksheet worksheet, ExcelRange cells, Color fontColor, Color backgroundColor, object value,
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
            if (backgroundColor != Color.Empty)
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
                this.SetPicturePosition(worksheet, cells, picture, rowIndex, columnIndex);

                //picture.SetPosition(
                //    rowIndex - 1,
                //    0,//(int)(EXCEL_ROW_HEIGHT / 2) - 150, // Row offset for placing image in cell center
                //    columnIndex - 1,
                //    0);//(int)(EXCEL_COLUMN_WIDTH / 2) - 150); // Column offset for placing image in cell center
            }
        }

        private void SetPicturePosition(ExcelWorksheet worksheet, ExcelRange cells, ExcelPicture picture, int rowIndex, int columnIndex)
        {
            double rowOffsetPixels = 0;//worksheet.Column(10).Width;
            double columnOffsetPixels = 0;// worksheet.Row(1).Height;

            picture.SetPosition(rowIndex - 1, (int)rowOffsetPixels, columnIndex - 1, (int)columnOffsetPixels);
        }
    }
}