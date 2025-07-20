using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;


class Program
{
    
    
    static void Main()
    {
        string folderPath = @"./EnglishData";
        InvertedIndex invertedIndex = new InvertedIndex();
        string[] files = OurFileReader.ReadAllFileNames(folderPath);

        foreach (string file in files)
        {
            invertedIndex.AddDocument(file);
        }
        string word = Console.ReadLine().ToUpper();
        var searchResult = invertedIndex.Search(word);
        foreach(string w in searchResult){
            Console.WriteLine(w);
        }
    }
}
