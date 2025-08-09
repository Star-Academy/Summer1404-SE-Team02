namespace SearchApplication;

public interface ISearchService
{
    public IEnumerable<string> Search(string rawQuery);
}