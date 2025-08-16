namespace PH01___C__tutorial.UniversityContexts;

public interface ICommonDbContext
{
    public int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}