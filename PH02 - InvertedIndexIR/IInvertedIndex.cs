public interface IInvertedIndex {
    void AddDocument(string path);
    LinkedList<string> Search(string str);
}