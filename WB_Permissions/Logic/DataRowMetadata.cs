using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WB_Permissions
{
    public class DataRowMetadata
    {
        public DataRow DataRow { get; set; }

        public int Id { get; set; }

        public List<FrameworkElement> Controls
        {
            get
            {
                return new List<FrameworkElement>()
                {
                    EntityNameTextBlock,
                    EntityLogicalNameTextBlock,
                    RoleTextBlock,
                    ReadImage,
                    WriteImage,
                    DeleteImage,
                    AppendImage,
                    AppendToImage,
                    AssignImage,
                    ShareImage,
                    Rectangle,
                };
            }
        }

        public TextBlock EntityNameTextBlock { get; set; }
        public TextBlock EntityLogicalNameTextBlock { get; set; }
        public TextBlock RoleTextBlock { get; set; }
        public Image ReadImage { get; set; }
        public Image WriteImage { get; set; }
        public Image DeleteImage { get; set; }
        public Image AppendImage { get; set; }
        public Image AppendToImage { get; set; }
        public Image AssignImage { get; set; }
        public Image ShareImage { get; set; }

        public RowDefinition GridRow { get; set; }

        public Rectangle Rectangle { get; set; }
    }
}
