using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Vapolia.KeyValueLite.Core
{
    public interface IKeyValueLiteLikeAkavache
    {
        Task<T> GetOrCreateObject<T>(string key, Func<T> create, [CallerMemberName]string caller = null);
        Task Invalidate(string key, [CallerMemberName]string caller = null);
        Task InsertObject<T>(string key, T value, DateTimeOffset? expiresOn = null, [CallerMemberName]string caller = null);
        Task<T> GetOrFetchObject<T>(string key, Func<Task<T>> loadCache, DateTimeOffset? expiresOn = null, [CallerMemberName]string caller = null);
        Task<T> GetObject<T>(string key, [CallerMemberName]string caller = null);
        Task<IEnumerable<T>> GetAllObjects<T>([CallerMemberName]string caller = null);
        Task InsertObjects<T>(Dictionary<string, T> keyValuePairs, [CallerMemberName]string caller = null);
        Task InvalidateAllObjects<T>([CallerMemberName]string caller = null);
        Task InvalidateAll([CallerMemberName]string caller = null);
    }
}