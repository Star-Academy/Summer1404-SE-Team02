using System.Collections.Generic;
using System.Linq;

public class SmartInvertedIndex : InvertedIndex
{

    public HashSet<string> SearchForOne(IEnumerable<string> words)
    {
        HashSet<string> result = new();
        foreach (var word in words)
        {
            var docs = Search(word);
            result.UnionWith(docs);
        }
        return result;
    }

    public HashSet<string> SearchForAll(IEnumerable<string> words)
    {
        HashSet<string>? result = null;
        foreach (var word in words)
        {
            var docs = new HashSet<string>(Search(word));
            if (result == null)
                result = docs;
            else
                result.IntersectWith(docs);
        }
        return result ?? new HashSet<string>();
    }

    public LinkedList<string> SmartSearch(Query query)
    {
        var must = SearchForAll(query.RequiredWords);
        var maybe = SearchForOne(query.OptionalWords);
        var forbidden = SearchForOne(query.ForbiddenWords);

        must.IntersectWith(maybe);
        must.ExceptWith(forbidden);

        return new LinkedList<string>(must);
    }
}
