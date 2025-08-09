using System.Collections.Generic;

public class InvertedIndexSearch : IInvertedIndexSearch
{
    private readonly ITokenizer _tokenizer;
    private readonly INormalizer _normalizer;

    public InvertedIndexSearch(ITokenizer tokenizer, INormalizer normalizer)
    {
        _tokenizer = tokenizer;
        _normalizer = normalizer;
    }
    

    public IEnumerable<string> Search(string word, InvertedIndex invertedIndex)
    {
        var words = _tokenizer.Tokenize(_normalizer.Normalize(word));
        var results = new HashSet<KeyValuePair<string, int>>();
        for (int i = 0; i < words.Length; i++)
        {
            if (invertedIndex.invertedIndex.ContainsKey(words[i].ToUpper()))
            {
                var list = new List<KeyValuePair<string, int>>(invertedIndex.invertedIndex[words[i].ToUpper()]);
                for (int j = 0; j < list.Count; j++)
                {
                    list[j] = new KeyValuePair<string, int>(list[j].Key, list[j].Value - i);
                } 
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
