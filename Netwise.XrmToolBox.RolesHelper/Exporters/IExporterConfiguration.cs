namespace Netwise.XrmToolBox.RolesHelper.Exporters
{
    /// <summary>
    /// Configuration for <see cref="IExporter"/>.
    /// </summary>
    public interface IExporterConfiguration<TData, TDataHolder>
    {
        /// <summary>
        /// Prepares data for <see cref="IExporter"/>.
        /// </summary>
        void PrepareData(TData data, TDataHolder dataHolder);
    }
}