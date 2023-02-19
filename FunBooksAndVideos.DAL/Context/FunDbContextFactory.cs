using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace FunBooksAndVideos.DAL.Context
{
    public class FunDbContextFactory : IDesignTimeDbContextFactory<FunDbContext>
    {
        const string connStr = "Server=tcp:funbooksandvideos-sql-server.database.windows.net,1433;Initial Catalog=funbooksandvideos-db;Persist Security Info=False;User ID=kkrastev;Password=!234Qwer;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public FunDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FunDbContext>();
            optionsBuilder.UseSqlServer(connStr);

            return new FunDbContext(optionsBuilder.Options);
        }
    }
}