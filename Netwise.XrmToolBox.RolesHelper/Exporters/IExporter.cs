namespace Netwise.XrmToolBox.RolesHelper.Exporters
{
    /// <summary>
    /// Exporter used to export data to specified destination.
    /// </summary>
    public interface IExporter<TDataHolder, TDestination, TData>
    {
        /// <summary>
        /// Export specified data to specified destination.
        /// </summary>
        void Export(TDataHolder dataHolder, TDestination destination, IExporterConfiguration<TData, TDataHolder> configuration);
    }
}