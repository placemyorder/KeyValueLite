using SQLite.Net2;

namespace Vapolia.KeyValueLite.Core
{
    public class DbMeta
    {
        [PrimaryKey]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
