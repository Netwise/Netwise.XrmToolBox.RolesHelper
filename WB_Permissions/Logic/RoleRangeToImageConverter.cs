using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WB_Permissions
{
    public class RoleRangeToImageConverter : IValueConverter
    {
        Uri NoneRoleRangeUri;
        Uri UserRoleRangeUri;
        Uri BuissnessUnitRoleRangeUri;
        Uri ParentBuisnessUnitRoleRangeUri;
        Uri OrganizationRoleRangeUri;

        BitmapImage NoneRoleRangeImage;
        BitmapImage UserRoleRangeImage;
        BitmapImage BuissnessUnitRoleRangeImage;
        BitmapImage ParentBuisnessUnitRoleRangeImage;
        BitmapImage OrganizationRoleRangeImage;

        public RoleRangeToImageConverter()
        {
            this.NoneRoleRangeUri = new Uri("Images/Empty.gif", UriKind.Relative);
            this.UserRoleRangeUri = new Uri("Images/User.gif", UriKind.Relative);
            this.BuissnessUnitRoleRangeUri = new Uri("Images/BusinessUnit.gif", UriKind.Relative);
            this.ParentBuisnessUnitRoleRangeUri = new Uri("Images/ParentBusinessUnit.gif", UriKind.Relative);
            this.OrganizationRoleRangeUri = new Uri("Images/Organization.gif", UriKind.Relative);

            this.NoneRoleRangeImage = new BitmapImage(NoneRoleRangeUri);
            this.UserRoleRangeImage = new BitmapImage(UserRoleRangeUri);
            this.BuissnessUnitRoleRangeImage = new BitmapImage(BuissnessUnitRoleRangeUri);
            this.ParentBuisnessUnitRoleRangeImage = new BitmapImage(ParentBuisnessUnitRoleRangeUri);
            this.OrganizationRoleRangeImage = new BitmapImage(OrganizationRoleRangeUri);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RoleRange range = (RoleRange)value;

            switch (range)
            {
                case RoleRange.None: return this.NoneRoleRangeImage;
                case RoleRange.User: return this.UserRoleRangeImage;
                case RoleRange.BusinessUnit: return this.BuissnessUnitRoleRangeImage;
                case RoleRange.ParentBusinessUnit: return this.ParentBuisnessUnitRoleRangeImage;
                case RoleRange.Organization: return this.OrganizationRoleRangeImage;
                default:
                    throw new NotImplementedException(string.Format("Role {0} no have image assigned.", range));
            }
        }

        public BitmapImage Convert(RoleRange value)
        {
            return (BitmapImage)Convert(value, null, null, CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
