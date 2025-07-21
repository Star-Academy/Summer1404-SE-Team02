using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class InvertedIndex : IInvertedIndex 
{
    private Dictionary<string, LinkedList<string>> invertedIndex = new Dictionary<string, LinkedList<string>>();
    var tokenizer = new BasicTokenizer();
    var normalizer = new BasicNormalizer();
    public void AddDocument(string txt) {
        string [] words;
        string normalizedText;
        normalizedText = normalizer.Normalize(txt);
        words = tokenizer.Tokenize(normalizedText);
        foreach(string word1 in words) {
            if(!invertedIndex.ContainsKey(word1)){
                invertedIndex.Add(word1, new LinkedList<string>());
                invertedIndex[word1].AddLast(address);
            }
            else if(invertedIndex[word1].Last() != address)
                invertedIndex[word1].AddLast(address);
        }
    }

    public LinkedList<string> Search(string word) {
        if (invertedIndex.TryGetValue(word, out var list)) {
            return list;
        }
        return new LinkedList<string>(); 
    }
}