namespace Netwise.XrmToolBox.RolesHelper.Exporters
{
    /// <summary>
    /// Exporter used to export data to specified destination.
    /// </summary>
    public interface IExporter<TDataHolder, TDestination, TData, TReturn>
    {
        /// <summary>
        /// Export specified data to specified destination.
        /// </summary>
        TReturn Export(TDataHolder dataHolder, TDestination destination, IExporterConfiguration<TData, TDataHolder> configuration);
    }
}