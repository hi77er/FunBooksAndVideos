using FunBooksAndVideos.DAL.Context;

namespace FunBooksAndVideos.WebApi.Handlers.Base
{
    public class DbHandler
    {
        protected readonly FunDbContext _dbContext;
        public DbHandler(FunDbContext dbContext) => _dbContext = dbContext;
    }
}
