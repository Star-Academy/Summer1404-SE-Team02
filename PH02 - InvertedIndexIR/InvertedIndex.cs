using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

class InvertedIndex 
{
    private Dictionary<string, LinkedList<string>> invertedIndex = new Dictionary<string, LinkedList<string>>();
    static string Normalize(string input) {
        string cleaned = Regex.Replace(input, @"\s+", " ").Trim();
        return Regex.Replace(cleaned, @"[^\w\s]", "").ToUpper();
    }
    static string[] Tokenize(string txt) {
        string noPunct = Normalize(txt);
        var outputText = noPunct.Split(" ");
        return outputText;
    }
    public void AddDocument(string address) {
        string txt;
        try
        {
            txt = File.ReadAllText(address);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: file could not be opened. {ex.Message}");
            return;
        }
        string [] words;
        words = Tokenize(txt);
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
        return new LinkedList<string>(); // return empty if not found
    }


    public HashSet<string> SearchForOne(LinkedList<string> words) {
        HashSet<string> searchResult = new HashSet<string>();
        foreach(string word in words) {
            searchResult.UnionWith(new HashSet<string>(Search(word)));
        }
        return searchResult;
    }


    public HashSet<string> SearchForAll(LinkedList<string> words) {
        HashSet<string>? searchResult = null;

        foreach (string word in words) {
            var currentSet = new HashSet<string>(Search(word));
            if (searchResult == null)
                searchResult = currentSet;
            else
                searchResult.IntersectWith(currentSet);
        }

        return searchResult ?? new HashSet<string>();
    }


    public LinkedList<string> SmartSearch(LinkedList<string> necessary, LinkedList<string> atLeastOne, LinkedList<string> forbidden) {
        var firstResult = SearchForAll(necessary);
        var secondResult = SearchForOne(atLeastOne);
        var thirdSearch = SearchForOne(forbidden);
        firstResult.IntersectWith(secondResult);
        var result = firstResult.Except(thirdSearch);
        return new LinkedList<string>(result);
    }
}