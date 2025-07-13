namespace Vapolia.KeyValueLite.Core
{
    public interface IDataStoreFactory
    {
        IDataStore CreateDataStore(string dataStoreName);
    }

    public class DataStore : BaseDataStore
    {
        [Preserve]
        public DataStore(IPlatformService ps) : base(ps)
        {
        }
    }

    #region infra
    public interface IPlatformService
    {
        string GetOrCreateDatabase(string dbPathName);
        string GetDatabaseFolder();
    }

    public abstract class BaseDataStore : IDataStore
    {
        public string DatabaseFilePathName { get; private set; }
        private IPlatformService ps;

        protected BaseDataStore(IPlatformService ps)
        {
            this.ps = ps;
        }

        public IDataStore Create(string dbPathName)
        {
            DatabaseFilePathName = ps.GetOrCreateDatabase(dbPathName);
            ps = null;
            return this;
        }
    }
    #endregion
}
