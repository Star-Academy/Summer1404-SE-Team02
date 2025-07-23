using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;


class Program
{

    static void Main()
    {
        string folderPath = "./EnglishData";

        var query = new Query();
        InvertedIndex invertedIndex = new InvertedIndex();
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
