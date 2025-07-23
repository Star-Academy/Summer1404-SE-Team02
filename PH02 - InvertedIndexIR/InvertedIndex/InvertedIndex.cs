using System;
using System.Collections;
using System.Dynamic;
using System.IO;
using System.Text.RegularExpressions;

public class InvertedIndex : IInvertedIndex
{
    private Dictionary<string, LinkedList<string>> invertedIndex = new Dictionary<string, LinkedList<string>>();
    private HashSet<string> documentNames = new HashSet<string>();
    private ITokenizer tokenizer = new BasicTokenizer();
    private INormalizer normalizer = new BasicNormalizer();

    public void AddDocument(string txt, string address)
    {
        string[] words;
        string normalizedText;
        normalizedText = normalizer.Normalize(txt);
        words = tokenizer.Tokenize(normalizedText);
        documentNames.Add(address);

        foreach (string word1 in words)
        {
            if (!invertedIndex.ContainsKey(word1))
            {
                invertedIndex.Add(word1, new LinkedList<string>());
                invertedIndex[word1].AddLast(address);
            }
            else if (invertedIndex[word1].Last() != address)
                invertedIndex[word1].AddLast(address);
        }
    }

    public IEnumerable<string> Search(string word)
    {
        if (invertedIndex.TryGetValue(word, out var list))
        {
            return list;
        }
        return new LinkedList<string>();
    }
    public IEnumerable<string> GetDocumentNames()
    {
        return new HashSet<string>(documentNames);
    }
}