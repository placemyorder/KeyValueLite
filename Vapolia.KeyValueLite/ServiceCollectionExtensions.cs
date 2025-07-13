using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SQLitePCL;
using Vapolia.KeyValueLite.Core;

namespace Vapolia.KeyValueLite
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKeyValueLite(this IServiceCollection services,
            string appName = nameof(Core.KeyValueLite))
        {
            // Init SQLite
            Batteries_V2.Init();
            DataStoreName.DbName = appName;
            // Register logger factory if not already registered
            services.AddLogging();

            // Register platform services and data store factory
            services.AddSingleton<IPlatformService, GenericPlatformService>();
            services.AddSingleton<IDataStoreFactory, DataStoreFactory>();
            services.AddSingleton<IKeyValueItemSerializer, KeyValueItemSytemTextJsonSerializer>();
            // Register KeyValueLite as a singleton, resolving dependencies from DI
            services.AddSingleton<IKeyValueLite,Core.KeyValueLite>();
            services.AddSingleton<IKeyValueLiteLikeAkavache, KeyValueLiteLikeAkavache>();

            return services;
        }
    }
}