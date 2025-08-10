using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;
using InvertedIndexIR.Filters;
using InvertedIndexIR.InputParser;
using InvertedIndexIR.InvertedIndexDocumentAdder;
using InvertedIndexIR.InvertedIndexSearch;
using InvertedIndexIR.QueryGetWordsOfType;
using InvertedIndexIR.QueryBuilder;
using InvertedIndexIR.Search.Extended;

[ExcludeFromCodeCoverage]
class Program
{

    private static string folderPath = "./EnglishData";
    private const string DefaultPattern = @"([+-]?""[^""]+""|[+-]?\S+)";
    private static List<string> notations = new List<string>() {"+", "-"};
    static void Main()
    {
        var queryBuileder = new QueryBuilder();
        var queryWordsOfTypeGetter = new QueryWordsOfTypeGetter();
        var inputParser = new InputParser();
        var tokenizer = new BasicTokenizer();
        var normalizer = new BasicNormalizer();
        var invertedIndex = new InvertedIndex();
        var indexSearch = new InvertedIndexSearch(tokenizer, normalizer);
        var indexAddDoc = new InvertedIndexDocumentAdder(tokenizer, normalizer);
        var extendedSearch = new ExtendedSearch();
        extendedSearch.AddFilter(new AtLeastOneFilter(indexSearch, queryWordsOfTypeGetter));
        extendedSearch.AddFilter(new NecessaryFilter(indexSearch,  queryWordsOfTypeGetter));
        extendedSearch.AddFilter(new ExcludedFilter(indexSearch, queryWordsOfTypeGetter));
        string[] files = FileReader.ReadAllFileNames(folderPath);
        Console.WriteLine("Files found: " + files.Length);
        foreach (string file in files)
        {
            indexAddDoc.AddDocument(File.ReadAllText(file), file, invertedIndex);
        }
        string input = Console.ReadLine() ?? "";
        var parsedInput = inputParser.ParseInput(input, DefaultPattern, notations);
        var query = queryBuileder.BuildQuery(parsedInput, notations);
        var searchResult = extendedSearch.Search(query, invertedIndex);
        foreach (string w in searchResult)
        {
           Console.WriteLine(w);
        }
        Console.WriteLine(searchResult.Count() + " documents found.");
    }
}
