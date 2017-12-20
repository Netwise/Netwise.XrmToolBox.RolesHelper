using OfficeOpenXml;
using System;
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
                    if (configuration != null)
                    {
                        configuration.PrepareData(package, data);
                        package.SaveAs(file);
                    }
                    else
                    {
                        throw new Exception("You must specify configuration for Exporter.");
                    }
                }
            }

            // Returns the same file but with different state (right now the file should exist).
            return new FileInfo(file.FullName);
        }
    }
}