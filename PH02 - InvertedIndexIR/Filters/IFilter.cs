
public interface IFilter
{
    public IEnumerable<string> ApplyFilter(IQuery query, IInvertedIndex invertedIndex);
}