using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.RegularExpressions;

[ExcludeFromCodeCoverage]
class Program
{

    static void Main()
    {
        string folderPath = "./EnglishData";

        var query = new Query();
        var tokenizer = new BasicTokenizer();
        var normalizer = new BasicNormalizer();
        InvertedIndex invertedIndex = new InvertedIndex(tokenizer, normalizer);
        var extendedSearch = new ExtendedSearch(invertedIndex);
        extendedSearch.AddFilter(new AtLeastOneFilter());
        extendedSearch.AddFilter(new NecessaryFilter());
        extendedSearch.AddFilter(new ExcludedFilter());
        string[] files = FileReader.ReadAllFileNames(folderPath);
        Console.WriteLine("Files found: " + files.Length);
        foreach (string file in files)
        {
            invertedIndex.AddDocument(File.ReadAllText(file), file);
        }
        string input = Console.ReadLine() ?? "";
        query.ParseInput(input);
        var searchResult = extendedSearch.Search(query);
        foreach (string w in searchResult)
        {
           // Console.WriteLine(w);
        }
        Console.WriteLine(searchResult.Count() + " documents found.");
    }
}
