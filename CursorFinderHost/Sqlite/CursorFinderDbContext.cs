using CursorFinder.Models;
using SQLite.CodeFirst;
using System.Data.Entity;

namespace CursorFinder.Sqlite
{
    public class CursorFinderDbContext : DbContext
    {
        public DbSet<CursorPosition> CursorPositions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<CursorFinderDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);

            var model = modelBuilder.Build(Database.Connection);
            ISqlGenerator sqlGenerator = new SqliteSqlGenerator();
            _ = sqlGenerator.Generate(model.StoreModel);
        }

    }
}
