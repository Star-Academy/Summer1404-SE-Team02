public interface IInvertedIndex
{
    void AddDocument(string path, string address);
    IEnumerable<string> Search(string str);
    public IEnumerable<string> GetDocumentNames();
}