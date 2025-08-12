using System.Collections.Generic;
using InvertedIndexIR.InvertedIndexSearch.Abstracion;
namespace InvertedIndexIR.InvertedIndexSearch;
public class InvertedIndexSearch : IInvertedIndexSearch
{
    private readonly ITokenizer _tokenizer;
    private readonly INormalizer _normalizer;

    public InvertedIndexSearch(ITokenizer tokenizer, INormalizer normalizer)
    {
        if(tokenizer == null) throw new ArgumentNullException(nameof(tokenizer));
        if(normalizer == null) throw new ArgumentNullException(nameof(normalizer));
        _tokenizer = tokenizer;
        _normalizer = normalizer;
    }
    

    public IReadOnlyCollection<string> Search(string word, InvertedIndex invertedIndex)
    {
        var words = _tokenizer.Tokenize(_normalizer.Normalize(word));
        var results = new HashSet<KeyValuePair<string, int>>();
        for (int i = 0; i < words.Length; i++)
        {
            if (invertedIndex.WordDocMap.ContainsKey(words[i].ToUpper()))
            {
                var list = invertedIndex.WordDocMap[words[i].ToUpper()]
                    .Select(kvp => new KeyValuePair<string, int>(kvp.Key, kvp.Value - i))
                    .ToList();

                if(i == 0)
                    results.UnionWith(list);
                else
                    results.IntersectWith(list);
            }
            else
                return new List<string>();
        }
        return results.Select(kvp => kvp.Key).Distinct().ToList();
    }
}
