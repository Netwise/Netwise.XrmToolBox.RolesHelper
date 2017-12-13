namespace Netwise.XrmToolBox.RolesHelper.Exporters
{
    /// <summary>
    /// Abstract implementation of <see cref="IExporter"/>.
    /// </summary>
    public abstract class AbstractExporter<TDataHolder, TDestination, TData> : IExporter<TDataHolder, TDestination, TData>
    {
        public abstract void Export(TDataHolder dataHolder, TDestination destination, IExporterConfiguration<TData, TDataHolder> configuration);
    }
}