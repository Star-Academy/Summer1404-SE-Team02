using System;
using System.Collections;
using System.Dynamic;
using System.IO;
using System.Text.RegularExpressions;
using InvertedIndexIR.InvertedIndexDocumentAdder.Abstraction;

namespace InvertedIndexIR.InvertedIndexDocumentAdder;

public class InvertedIndexDocumentAdder : IInvertedIndexDocumentAdder
{
    private readonly ITokenizer _tokenizer;
    private readonly INormalizer _normalizer;
    public InvertedIndexDocumentAdder(ITokenizer tokenizer, INormalizer normalizer)
    {
        if (tokenizer == null) throw new ArgumentNullException(nameof(tokenizer));
        if (normalizer == null) throw new ArgumentNullException(nameof(normalizer));
        this._tokenizer = tokenizer;
        this._normalizer = normalizer;
    }
    public void AddDocument(string txt, string address, InvertedIndex invertedIndex)
    {
        var normalizedText = _normalizer.Normalize(txt);
        var words = _tokenizer.Tokenize(normalizedText);
        invertedIndex.documentNames.Add(address);

        for (int i = 0; i < words.Length; i++)
        {
            if (!invertedIndex.wordDocMap.ContainsKey(words[i]))
            {
                invertedIndex.wordDocMap.Add(words[i], new List<KeyValuePair<string, int>>());
            }
            invertedIndex.wordDocMap[words[i]].Add(new KeyValuePair<string, int>(address, i));
        }
    }
}