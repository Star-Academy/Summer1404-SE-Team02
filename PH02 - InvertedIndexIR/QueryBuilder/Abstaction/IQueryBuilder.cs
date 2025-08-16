using InvertedIndexIR.DTO;

namespace InvertedIndexIR.QueryBuilder.Abstraction;

public interface IQueryBuilder
{
    public Query BuildQuery(List<string> parsedWords, List<string> notations);
}