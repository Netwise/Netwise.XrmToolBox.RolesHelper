namespace Netwise.XrmToolBox.RolesHelper.Exporters
{
    /// <summary>
    /// Abstract implementation of <see cref="IExporterConfiguration"/>.
    /// </summary>
    public abstract class AbstractExporterConfiguration<TData, TDataHolder> : IExporterConfiguration<TData, TDataHolder>
    {
        public abstract void PrepareData(TData data, TDataHolder dataHolder);
    }
}