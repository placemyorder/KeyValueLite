using System.Text.Json;
using System.Text.Json.Serialization;
using Vapolia.KeyValueLite.Core;

namespace Vapolia.KeyValueLite
{
    public class KeyValueItemSytemTextJsonSerializer : IKeyValueItemSerializer
    {
        //private readonly JsonSerializer serializer;

        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true,
        };

        public T GetValue<T>(KeyValueItem kvi)
        {
            if (kvi.Value == null)
                return default;

            return JsonSerializer.Deserialize<T>(kvi.Value, jsonOptions);
        }

        public T GetValue<T>(string stringValue)
        {
            if (stringValue == null)
                return default;

            return JsonSerializer.Deserialize<T>(stringValue, jsonOptions);
        }

        public string SerializeToString(object value)
        {
            if (value == null)
                return null;
            return JsonSerializer.Serialize(value, jsonOptions);
        }
    }
}
