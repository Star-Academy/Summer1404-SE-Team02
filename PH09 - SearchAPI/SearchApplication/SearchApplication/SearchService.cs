using System.Collections.Generic;
using InvertedIndexWebApi.Filters;
using InvertedIndexWebApi.Query;
using InvertedIndexWebApi.InvertedIndexDocumentSearch;
using InvertedIndexWebApi.ParseInput;
using InvertedIndexWebApi;
using InvertedIndexWebApi.ExtendedSearch;
using InvertedIndexWebApi.FileReader;
using InvertedIndexWebApi.InvertedIndexDTO;
using InvertedIndexWebApi.InvertedIndexDocumentAdder;
using InvertedIndexWebApi.Normalizer;
using InvertedIndexWebApi.Tokenizer;
using SearchApplication;

public class SearchService :ISearchService
{
    private readonly InvertedIndex _index;
    private readonly IExtendedSearch _extendedSearch;
    private readonly IInputParser _parser;

    public SearchService()
    {
        var tokenizer = new BasicTokenizer();
        var normalizer = new BasicNormalizer();
        _parser = new InputParser();
        _index = new InvertedIndex();

        var adder = new InvertedIndexDocumentAdder(tokenizer, normalizer);
        var filePaths = FileReader.ReadAllFileNames("Search\\EnglishData"); 

        foreach (var path in filePaths)
        {
            var content = File.ReadAllText(path);
            adder.AddDocument(content, path, _index);
        }
        Console.WriteLine(filePaths.Length + "FILES FOUND");
        Console.WriteLine(Directory.GetCurrentDirectory() + " current directory");

        var indexSearch = new InvertedIndexSearch(tokenizer, normalizer);

        _extendedSearch = new ExtendedSearch();
        _extendedSearch.AddFilter(new AtLeastOneFilter(indexSearch));
        _extendedSearch.AddFilter(new NecessaryFilter(indexSearch));
        _extendedSearch.AddFilter(new ExcludedFilter(indexSearch));
    }

    public IEnumerable<string> Search(string rawQuery)
    {
        var parsedWords = _parser.ParseInput(rawQuery, @"[+-]?[\""].+?[\""]|\S+", new List<string> { "+", "-" });
        var query = new Query(parsedWords);
        return _extendedSearch.Search(query, _index);
    }
}
