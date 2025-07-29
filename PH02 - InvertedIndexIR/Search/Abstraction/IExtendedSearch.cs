using System.Reflection;

public interface IExtendedSearch
{
    public IEnumerable<string> Search(IQuery query, InvertedIndex index);
    public void AddFilter(IFilter filter);
}