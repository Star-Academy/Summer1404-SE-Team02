namespace InvertedIndexWebApi.InvertedIndexDocumentAdder;
using InvertedIndexWebApi.InvertedIndexDTO;

public interface IInvertedIndexDocumentAdder
{
    void AddDocument(string path, string address, InvertedIndex invertedIndex);
}