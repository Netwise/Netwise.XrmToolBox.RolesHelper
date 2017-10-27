using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WB_Permissions
{
    /// <summary>
    /// Interaction logic for WB_Permissions.xaml
    /// </summary>
    public partial class WB_Permissions : UserControl
    {
        int lastId = 0;
        public SortCase LastSortCase { get; set; }
        public RoleRangeToImageConverter Converter { get; set; }
        public List<DataRow> Rows { get; set; }
        public List<DataRowMetadata> RowsMetadata { get; set; }

        public WB_Permissions()
        {
            this.InitializeComponent();

            this.Rows = new List<DataRow>();
            this.RowsMetadata = new List<DataRowMetadata>();
            this.Converter = new RoleRangeToImageConverter();
            this.Sort(SortCase.ByEntityNameAsc);
            //this.CreateTestData();
        }

        private void CreateTestData()
        {
            this.AddRow(new DataRow()
            {
                EntityName = "A",
                EntityLogicalName = "a",
                Role = "aaa",
                Read = RoleRange.None,
                Write = RoleRange.BusinessUnit,
                Delete = RoleRange.None,
                Append = RoleRange.None,
                AppendTo = RoleRange.None,
                Assign = RoleRange.None,
                Share = RoleRange.None,
            });


            this.AddRow(new DataRow()
            {
                EntityName = "C",
                EntityLogicalName = "c",
                Role = "aaa",
                Read = RoleRange.None,
                Write = RoleRange.BusinessUnit,
                Delete = RoleRange.None,
                Append = RoleRange.None,
                AppendTo = RoleRange.None,
                Assign = RoleRange.None,
                Share = RoleRange.None,
            });

            this.AddRow(new DataRow()
            {
                EntityName = "B",
                EntityLogicalName = "a",
                Role = "aaa",
                Read = RoleRange.None,
                Write = RoleRange.BusinessUnit,
                Delete = RoleRange.None,
                Append = RoleRange.None,
                AppendTo = RoleRange.None,
                Assign = RoleRange.None,
                Share = RoleRange.None,
            });
        }

        public void AddRow(DataRow dataRow)
        {
            //Get data and create/init needed objects
            RowDefinition newRow = new RowDefinition() { Height = GridLength.Auto, };
            int currentRowNumber = this.MainContent.RowDefinitions.Count - 1;
            DataRowMetadata metadata = new DataRowMetadata()
            {
                Id = this.lastId++,
                DataRow = dataRow,
                GridRow = newRow,
                EntityNameTextBlock = new TextBlock() { Text = dataRow.EntityName, Style = (Style)Resources["TableContent"], },
                EntityLogicalNameTextBlock = new TextBlock() { Text = dataRow.EntityLogicalName, Style = (Style)Resources["TableContent"], },
                RoleTextBlock = new TextBlock() { Text = dataRow.Role, Style = (Style)Resources["TableContent"], },
                ReadImage = new Image() { Source = this.Converter.Convert(dataRow.Read), ToolTip = dataRow.Read, Style = (Style)Resources["RoleStatus"], },
                WriteImage = new Image() { Source = this.Converter.Convert(dataRow.Write), ToolTip = dataRow.Write, Style = (Style)Resources["RoleStatus"], },
                DeleteImage = new Image() { Source = this.Converter.Convert(dataRow.Delete), ToolTip = dataRow.Delete, Style = (Style)Resources["RoleStatus"], },
                AppendImage = new Image() { Source = this.Converter.Convert(dataRow.Append), ToolTip = dataRow.Append, Style = (Style)Resources["RoleStatus"], },
                AppendToImage = new Image() { Source = this.Converter.Convert(dataRow.AppendTo), ToolTip = dataRow.AppendTo, Style = (Style)Resources["RoleStatus"], },
                AssignImage = new Image() { Source = this.Converter.Convert(dataRow.Assign), ToolTip = dataRow.Assign, Style = (Style)Resources["RoleStatus"], },
                ShareImage = new Image() { Source = this.Converter.Convert(dataRow.Share), ToolTip = dataRow.Share, Style = (Style)Resources["RoleStatus"], },
                Rectangle = new Rectangle() { Style = (Style)Resources["Line"], },
            };

            Grid.SetColumn(metadata.EntityNameTextBlock, 0);
            Grid.SetColumn(metadata.EntityLogicalNameTextBlock, 1);
            Grid.SetColumn(metadata.RoleTextBlock, 2);
            Grid.SetColumn(metadata.ReadImage, 3);
            Grid.SetColumn(metadata.WriteImage, 4);
            Grid.SetColumn(metadata.DeleteImage, 5);
            Grid.SetColumn(metadata.AppendImage, 6);
            Grid.SetColumn(metadata.AppendToImage, 7);
            Grid.SetColumn(metadata.AssignImage, 8);
            Grid.SetColumn(metadata.ShareImage, 9);

            //Add to table data row
            this.RowsMetadata.Add(metadata);
            this.Rows.Add(dataRow);
            this.MainContent.RowDefinitions.Add(newRow);
            foreach (FrameworkElement control in metadata.Controls)
            {
                Grid.SetRow(control, currentRowNumber);
                this.MainContent.Children.Add(control);
            }
        }
        public void AddRange(IEnumerable<DataRow> collection)
        {
            foreach (DataRow row in collection)
            {
                this.AddRow(row);
            }
        }

        public void Remove(DataRow dataRow)
        {
            DataRowMetadata metadata = this.RowsMetadata.First(dr => dr.DataRow.Equals(dataRow));
            foreach (FrameworkElement control in metadata.Controls)
            {
                this.MainContent.Children.Remove(control);
            }
            this.MainContent.RowDefinitions.Remove(metadata.GridRow);
            this.Rows.Remove(metadata.DataRow);
            this.RowsMetadata.Remove(metadata);
        }
        public void RemoveAll()
        {
            foreach (DataRowMetadata metadata in this.RowsMetadata)
            {
                foreach (FrameworkElement control in metadata.Controls)
                {
                    this.MainContent.Children.Remove(control);
                }
            }

            this.Rows.Clear();
            this.RowsMetadata.Clear();
        }

        public void Sort(SortCase sortCase)
        {
            List<DataRow> rows = this.Rows.ToList();
            this.RemoveAll();

            switch (sortCase)
            {
                case SortCase.ByEntityNameAsc: rows = rows.OrderBy(r => r.EntityName).ToList(); break;
                case SortCase.ByEntityNameDesc: rows = rows.OrderByDescending(r => r.EntityName).ToList(); break;
                case SortCase.ByEntityLogicalNameAsc: rows = rows.OrderBy(r => r.EntityLogicalName).ToList(); break;
                case SortCase.ByEntityLogicalNameDesc: rows = rows.OrderByDescending(r => r.EntityLogicalName).ToList(); break;
                case SortCase.ByRoleAsc: rows = rows.OrderBy(r => r.Role).ToList(); break;
                case SortCase.ByRoleDesc: rows = rows.OrderByDescending(r => r.Role).ToList(); break;
                default:
                    throw new NotImplementedException("Not implemented sorting case");
            }

            this.LastSortCase = sortCase;
            this.AddRange(rows);
        }

        private void EntityNameSortArrowButton_Click(object sender, RoutedEventArgs e)
        {
            if (LastSortCase == SortCase.ByEntityNameAsc)
            {
                this.Sort(SortCase.ByEntityNameDesc);
            }
            else
            {
                this.Sort(SortCase.ByEntityNameAsc);
            }
        }

        private void EntityLogicalNameSortArrowButton_Click(object sender, RoutedEventArgs e)
        {
            if (LastSortCase == SortCase.ByEntityLogicalNameAsc)
            {
                this.Sort(SortCase.ByEntityLogicalNameDesc);
            }
            else
            {
                this.Sort(SortCase.ByEntityLogicalNameAsc);
            }
        }

        private void RoleSortArrowButton_Click(object sender, RoutedEventArgs e)
        {
            if (LastSortCase == SortCase.ByRoleAsc)
            {
                this.Sort(SortCase.ByRoleDesc);
            }
            else
            {
                this.Sort(SortCase.ByRoleAsc);
            }
        }
    }
}
