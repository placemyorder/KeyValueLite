using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Vapolia.KeyValueLite.Core
{
    public interface IKeyValueLite : IDisposable
    {
        /// <summary>
        /// Retrieves a <code>KeyValueItem</code> based on the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>KeyValueItem if it exists or null otherwise. 
        /// If an expiration was configured and is past the current time, it will also return null.</returns>
        Task<KeyValueItem> Get(string key);

        /// <summary>
        /// Retrieves a strongly typed value corresponding to a key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The value corresponding to the key if it exists or null otherwise. 
        /// If an expiration was configured and is past the current time, it will also return null.</returns>
        Task<T> Get<T>(string key);

        Task<T> GetOrCreateObject<T>(string key, Func<T> create);
        Task<T> GetOrFetchObject<T>(string key, Func<Task<T>> create, DateTimeOffset? expiresOn = null);
        Task<List<T>> GetAll<T>();
        Task RemoveAll<T>();
        Task RemoveAll();

        /// <summary>
        /// Persists the specified <code>KeyValueItem</code>, updating it if the key already exists.
        /// </summary>
        /// <param name="keyValueItem">The key value item.</param>
        Task Set(KeyValueItem keyValueItem);

        /// <summary>
        /// Persists the specified key, value and expiration, updating it if the key already exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiresOn">The expire date after which this key is no longer valid.</param>
        Task Set(string key, object value, DateTimeOffset? expiresOn = null);

        Task InsertObjects<T>(Dictionary<string, T> keyValuePairs, DateTimeOffset? expiresOn = null);

        /// <summary>
        /// Removes the specified key value item.
        /// </summary>
        /// <param name="keyValueItem">The key value item to remove.</param>
        Task Remove(KeyValueItem keyValueItem);

        /// <summary>
        /// Removes the specified key value item.
        /// </summary>
        /// <param name="key">The key.</param>
        Task Remove(string key);
    }
}