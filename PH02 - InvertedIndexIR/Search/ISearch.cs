using System.Reflection;

public interface ISearch
{
    public IEnumerable<string> Search(IQuery query);
    public void AddFilter(IFilter filter);
}