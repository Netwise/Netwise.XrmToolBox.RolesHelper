using System;
using System.IO;
using System.Windows.Media.Imaging;

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
            MemoryStream ms = new MemoryStream();
            BmpBitmapEncoder bbe = new BmpBitmapEncoder();
            bbe.Frames.Add(BitmapFrame.Create(new Uri(image.Source.ToString(), UriKind.RelativeOrAbsolute)));
            bbe.Save(ms);
            System.Drawing.Image newImage = System.Drawing.Image.FromStream(ms);
            return newImage;
        }
    }
}