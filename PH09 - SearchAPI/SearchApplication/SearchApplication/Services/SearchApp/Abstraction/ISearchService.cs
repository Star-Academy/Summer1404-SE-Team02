namespace SearchApp.Abstraction;

public interface ISearchService
{
    public IEnumerable<string> Search(string rawQuery);
}