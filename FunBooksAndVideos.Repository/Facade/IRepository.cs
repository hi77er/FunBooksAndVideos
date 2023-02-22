namespace FunBooksAndVideos.Repository.Facade
{
    public interface IRepository
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
