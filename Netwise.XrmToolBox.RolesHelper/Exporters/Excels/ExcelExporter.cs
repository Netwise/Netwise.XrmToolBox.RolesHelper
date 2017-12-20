using OfficeOpenXml;
using System.IO;
using System.Windows.Forms;

namespace Netwise.XrmToolBox.RolesHelper.Exporters.Excels
{
    public class ExcelExporter : IExporter<PluginControl, string, ExcelPackage, FileInfo>
    {
        public FileInfo Export(PluginControl data, string destination, IExporterConfiguration<ExcelPackage, PluginControl> configuration)
        {
            FileInfo file = new FileInfo(destination);
            if (file.Exists)
            {
                MessageBox.Show("File with specified name already exists. Specify new name.");
            }
            else
            {
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    configuration.PrepareData(package, data);
                    package.SaveAs(file);
                }
            }
            return file;
        }
    }
}