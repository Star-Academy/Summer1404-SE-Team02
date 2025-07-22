public interface IInvertedIndex {
    void AddDocument(string path, string address);
    LinkedList<string> Search(string str);
}