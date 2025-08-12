namespace InvertedIndexIR.QueryGetWordsOfType.Abstraction;
using InvertedIndexIR.DTO;

public interface IQueryWordsOfTypeGetter
{
    public List<string> GetWordsOfType(Query query, string notation);
}