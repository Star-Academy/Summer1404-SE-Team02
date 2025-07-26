using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
[ExcludeFromCodeCoverage]
public class FileReader
{
    public static string[] ReadAllFileNames(string address) {
        string[] files = {};
        if (Directory.Exists(address)){
            files = Directory.GetFiles(address);
        }
        return files;
    }  
}
