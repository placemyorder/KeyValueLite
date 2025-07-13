﻿using Microsoft.Extensions.Logging;
using SQLite.Net2;

namespace Vapolia.KeyValueLite.Core
{
    internal class KeyValueDbContext : DbContextBase<KeyValueDbContext>
    {
        protected override int CurrentSchemaVersion => 2;

        protected override void CreateUserTables(SQLiteConnection db)
        {
            db.CreateTableIfNotExist<KeyValueItem>();
        }

        public void Clear()
        {
            DropTable<KeyValueItem>();
            CreateTable<KeyValueItem>();
        }

        public KeyValueDbContext(IDataStore dataStore, ILogger logger) : base(dataStore, logger)
        {
        }
    }
}