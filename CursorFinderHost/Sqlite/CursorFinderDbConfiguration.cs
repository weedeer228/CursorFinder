using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity;
using System.Data.SQLite;
using System.Data.SQLite.EF6;

namespace CursorFinder.Sqlite
{
    internal class CursorFinderDbConfiguration : DbConfiguration
    {
        public CursorFinderDbConfiguration()
        {
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);

            var providerServices = (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices));

            SetProviderServices("System.Data.SQLite", providerServices);
            SetProviderServices("System.Data.SQLite.EF6", providerServices);
        }

        public DbConnection CreateConnection(string connectionString)
            => new SQLiteConnection(connectionString);
    }
}
