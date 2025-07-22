using System.Collections.Generic;
using System.Linq;

public class SmartInvertedIndex : InvertedIndex
{

    public HashSet<string> SearchForOne(LinkedList<string> words)
    {
        HashSet<string> result = new();
        foreach (var word in words)
        {
            var docs = Search(word);
            result.UnionWith(docs);
        }
        return result;
    }

    public HashSet<string> SearchForAll(LinkedList<string> words)
    {
        HashSet<string>? result = null;
        foreach (var word in words)
        {
            var docs = new HashSet<string>(Search(word));
            // Console.WriteLine($"Searching for '{word}': {docs.Count} documents found.");
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
                Console.WriteLine($"Search results: {maybe.Count} documents found.");

        if(query.OptionalWords.Count != 0)
        {
            must.IntersectWith(maybe);
        }
        must.ExceptWith(forbidden);
        return new LinkedList<string>(must);
    }
}
