using Netwise.XrmToolBox.RolesHelper.Properties;
using System;

namespace Netwise.XrmToolBox.RolesHelper.Exporters.Excels
{
    public static class Extensions
    {
        private delegate System.Drawing.Image GetDrawingImage(System.Windows.Controls.Image image);

        public static System.Drawing.Image ToDrawingImage(this System.Windows.Controls.Image image)
        {
            GetDrawingImage del = new GetDrawingImage(ConvertImage);
            System.Drawing.Image newImage = (System.Drawing.Image)image.Dispatcher.Invoke(del, new object[] { image });
            return newImage;
        }

        private static System.Drawing.Image ConvertImage(this System.Windows.Controls.Image image)
        {
            var imagePath = image.Source.ToString();

            if (imagePath.EndsWith("BusinessUnit.gif")) return Resources.BusinessUnit;
            else if (imagePath.EndsWith("Empty.gif")) return Resources.Empty;
            else if (imagePath.EndsWith("Organization.gif")) return Resources.Organization;
            else if (imagePath.EndsWith("ParentBusinessUnit.gif")) return Resources.ParentBusinessUnit;
            else if (imagePath.EndsWith("User.gif")) return Resources.User;
            else throw new Exception($"Unknown image: { imagePath }");
        }
    }
}