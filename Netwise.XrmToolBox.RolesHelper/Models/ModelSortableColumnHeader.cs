using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Windows.Forms;

namespace Netwise.XrmToolBox.RolesHelper.Models
{
    /// <summary>
    /// Describes the Table Header which can be used for sorting.
    /// </summary>
    public class ModelSortableColumnHeader
    {
        /// <summary>
        /// Column Header name: "Entity Name", "Entity Logical Name", etc.
        /// </summary>
        public string HeaderName { get; }
        public int ColumnIndex { get; }
        /// <summary>
        /// Use to determine by what elements in this column should be sort.
        /// </summary>
        public Func<EntityMetadata, object> OrderBy { get; }

        /// <summary>
        /// Why private ?
        /// See static ModelSortableColumnHeader.New method
        /// </summary>
        private ModelSortableColumnHeader(string headerName, int columnIndex, Func<EntityMetadata, object> orderBy)
        {
            this.HeaderName = headerName;
            this.ColumnIndex = columnIndex;
            this.OrderBy = orderBy;
        }

        /// <summary>
        /// Converts current Header to HTML Element.
        /// </summary>
        public string ToHtmlElement()
        {
            // Currently used sorting Icon
            var sortIcon = "https://cdn1.iconfinder.com/data/icons/materia-arrows-symbols-vol-2/24/018_059_arrow_sort_exchange_sorting-512.png";
            // For more details about HTML elements see: HtmlHelper.PrepareHtmlHeader(...)
            string element = $"<th>{ HeaderName } <img src=\"{ sortIcon }\" class=\"sortIcon\" onclick=\"sortTable(\"MainTable\", 0)\" /></th>";
            return element;
        }

        /// <summary>
        /// Add name to Sort ComboBox if possible.
        /// </summary>
        private static void AddToSortingListIfPossible(string headerName, ComboBox CB_Sort)
        {
            if (CB_Sort.Items.Contains(headerName))
            {
                return;
            }
            else
            {
                CB_Sort.Items.Add(headerName);
            }
        }
    }
}