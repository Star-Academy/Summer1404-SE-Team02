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
        SmartInvertedIndex invertedIndex = new SmartInvertedIndex();
        string[] files = FileReader.ReadAllFileNames(folderPath);
        Console.WriteLine("Files found: " + files.Length);
        foreach (string file in files)
        {
            invertedIndex.AddDocument(File.ReadAllText(file), file);
        }

        string input = Console.ReadLine() ?? "";
        query.ParseInput(input);
        var searchResult = invertedIndex.SmartSearch(query);
        foreach(string w in searchResult){
            Console.WriteLine(w);
        }
    }
}
