namespace InvertedIndexIR.QueryBuilder.Abstraction;
using InvertedIndexIR.Query;

public interface IQueryBuilder
{
    public Query BuildQuery(List<string> parsedWords, List<string> notations);
}