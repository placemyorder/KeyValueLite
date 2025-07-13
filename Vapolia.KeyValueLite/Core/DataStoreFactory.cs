using System;
using System.IO;

namespace Vapolia.KeyValueLite.Core
{
    public class DataStoreFactory : IDataStoreFactory
    {
        private readonly IPlatformService platformService;
        private readonly string baseFolder;

        public DataStoreFactory(IPlatformService platformService)
        {
            this.platformService = platformService;
            baseFolder = platformService.GetDatabaseFolder();
        }

        IDataStore IDataStoreFactory.CreateDataStore(string dataStoreName) 
            => new DataStore(platformService).Create(GetDataStorePathName(dataStoreName));

        public virtual string GetDataStorePathName(string dataStoreName)
        {
            if (string.IsNullOrWhiteSpace(dataStoreName))
            {
                throw new ArgumentException("Unknown data store, or not implemented", nameof(dataStoreName));
            }

            return dataStoreName switch
            {
                nameof(KeyValueLite) => "keyvaluecache.db",
                _ => dataStoreName
            };
        }

        protected virtual string GetDbPathNameForCurrentUser(string dbName)
        {
            var userFolder = baseFolder; //Path.Combine(baseFolder,userSession.UserFolderName);
            var dbPathName = Path.Combine(userFolder, dbName);

            if (!Directory.Exists(userFolder))
                Directory.CreateDirectory(userFolder);

            return dbPathName;
        }
    }
}