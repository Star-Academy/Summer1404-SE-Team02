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
        string folderPath = "C:\\Users\\RSKALA\\Desktop\\codeStar\\PH02 - InvertedIndexIR\\EnglishData";

        var query = new Query();
        var tokenizer = new BasicTokenizer();
        var normalizer = new BasicNormalizer();
        var invertedIndex = new InvertedIndex();
        var indexSearch = new InvertedIndexSearch(tokenizer, normalizer);
        var indexAddDoc = new InvertedIndexDocumentAdder(tokenizer, normalizer);
        var extendedSearch = new ExtendedSearch();
        extendedSearch.AddFilter(new AtLeastOneFilter(indexSearch));
        extendedSearch.AddFilter(new NecessaryFilter(indexSearch));
        extendedSearch.AddFilter(new ExcludedFilter(indexSearch));
        string[] files = FileReader.ReadAllFileNames(folderPath);
        Console.WriteLine("Files found: " + files.Length);
        foreach (string file in files)
        {
            indexAddDoc.AddDocument(File.ReadAllText(file), file, invertedIndex);
        }
        string input = Console.ReadLine() ?? "";
        query.ParseInput(input);
        var searchResult = extendedSearch.Search(query, invertedIndex);
        foreach (string w in searchResult)
        {
           Console.WriteLine(w);
        }
        Console.WriteLine(searchResult.Count() + " documents found.");
    }
}
