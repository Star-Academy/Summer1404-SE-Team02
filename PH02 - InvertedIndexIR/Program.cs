using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;


class Program
{
    
    public static void SplitAndCategorize(
    string line,
    LinkedList<string> normal,
    LinkedList<string> plus,
    LinkedList<string> minus)
    {
        string[] tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var token in tokens)
        {
            if (token.StartsWith("+"))
            {
                plus.AddLast(token.Substring(1, token.Length - 1));
            }
            else if (token.StartsWith("-"))
            {
                minus.AddLast(token.Substring(1, token.Length - 1));
            }
            else
            {
                normal.AddLast(token);
            }
        }
    }

    static void Main()
    {
        string folderPath = @"./EnglishData";
        var normal = new LinkedList<string>();
        var plus = new LinkedList<string>();
        var minus = new LinkedList<string>();
        InvertedIndex invertedIndex = new InvertedIndex();
        string[] files = FileReader.ReadAllFileNames(folderPath);

        foreach (string file in files)
        {
            invertedIndex.AddDocument(file);
        }
        string words = Console.ReadLine().ToUpper();
        SplitAndCategorize(words, normal, plus, minus);
        var searchResult = invertedIndex.SmartSearch(normal, plus, minus);
        foreach(string w in searchResult){
            Console.WriteLine(w);
        }
    }
}
