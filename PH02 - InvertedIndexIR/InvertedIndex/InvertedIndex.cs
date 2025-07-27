using System;
using System.Collections;
using System.Dynamic;
using System.IO;
using System.Text.RegularExpressions;

public class InvertedIndex : IInvertedIndex
{
    public Dictionary<string, LinkedList<KeyValuePair<string, int>>> invertedIndex = new Dictionary<string, LinkedList<KeyValuePair<string, int>>>();
    private HashSet<string> documentNames = new HashSet<string>();
    private ITokenizer tokenizer;
    private INormalizer normalizer;
    public InvertedIndex(ITokenizer tokenizer, INormalizer normalizer)
    {
        this.tokenizer = tokenizer;
        this.normalizer = normalizer;
    }
    public InvertedIndex()
    {
        this.tokenizer = new BasicTokenizer();
        this.normalizer = new BasicNormalizer();
    }
    public void AddDocument(string txt, string address)
    {
        string[] words;
        string normalizedText;
        normalizedText = normalizer.Normalize(txt);
        words = tokenizer.Tokenize(normalizedText);
        documentNames.Add(address);

        for (int i = 0; i < words.Length; i++)
        {
            if (!invertedIndex.ContainsKey(words[i]))
            {
                invertedIndex.Add(words[i], new LinkedList<KeyValuePair<string, int>>());
            }
            invertedIndex[words[i]].AddLast(new KeyValuePair<string, int>(address, i));
        }
    }

    public IEnumerable<string> Search(string word)
    {
        var normalizedText = normalizer.Normalize(word);
        var words = tokenizer.Tokenize(normalizedText);
        var results = new HashSet<KeyValuePair<string, int>>();
        for (int i = 0; i < words.Length; i++)
        {
            if (invertedIndex.ContainsKey(words[i].ToUpper()))
            {
                var list = new List<KeyValuePair<string, int>>(invertedIndex[words[i].ToUpper()]);
                for (int j = 0; j < list.Count; j++)
                {
                    list[j] = new KeyValuePair<string, int>(list[j].Key, list[j].Value - i);
                } 
                if(i == 0)
                {
                    results.UnionWith(list);
                }
                else
                {
                    results.IntersectWith(list);
                }
            }
            else
            {
                return new LinkedList<string>();
            }
        }

        return results.Select(kvp => kvp.Key).Distinct().ToList();
    }
    public IEnumerable<string> GetDocumentNames()
    {
        return new HashSet<string>(documentNames);
    }
}