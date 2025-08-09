using System;
using System.Collections;
using System.Dynamic;
using System.IO;
using System.Text.RegularExpressions;

namespace InvertedIndexWebApi.InvertedIndexDocumentAdder;
using InvertedIndexWebApi.InvertedIndexDTO;
using InvertedIndexWebApi.Normalizer;
using InvertedIndexWebApi.Tokenizer;

public class InvertedIndexDocumentAdder : IInvertedIndexDocumentAdder
{
    private ITokenizer tokenizer;
    private INormalizer normalizer;
    
    public InvertedIndexDocumentAdder(ITokenizer tokenizer, INormalizer normalizer)
    {
        this.tokenizer = tokenizer;
        this.normalizer = normalizer;
    }
    public void AddDocument(string txt, string address, InvertedIndex invertedIndex)
    {
        var normalizedText = normalizer.Normalize(txt);
        var words = tokenizer.Tokenize(normalizedText);
        invertedIndex.documentNames.Add(address);

        for (int i = 0; i < words.Length; i++)
        {
            if (!invertedIndex.invertedIndex.ContainsKey(words[i]))
            {
                invertedIndex.invertedIndex.Add(words[i], new List<KeyValuePair<string, int>>());
            }
            invertedIndex.invertedIndex[words[i]].Add(new KeyValuePair<string, int>(address, i));
        }
    }
}