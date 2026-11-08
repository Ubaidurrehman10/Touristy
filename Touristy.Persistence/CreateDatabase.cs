using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Touristy.Persistence
{
    public static class CreateDatabase
    {
        public static void EnsureDatabaseCreated(SqliteConnection conn)
        {
            var builder = new DbContextOptionsBuilder<TouristyAppDbContext>();
            builder.UseSqlite(conn);


            using var context = new TouristyAppDbContext(builder.Options);
            context.Database.EnsureCreated();
        }
    }
}
