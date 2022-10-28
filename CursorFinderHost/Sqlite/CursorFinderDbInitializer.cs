using SQLite.CodeFirst;
using System.Data.Entity;

namespace CursorFinder.Sqlite
{
    internal class CursorFinderDbInitializer : SqliteDropCreateDatabaseAlways<CursorFinderDbContext>
    {
        public CursorFinderDbInitializer(DbModelBuilder modelBuilder)
        : base(modelBuilder) { }

        protected override void Seed(CursorFinderDbContext context)
        {
        }
    }

}
