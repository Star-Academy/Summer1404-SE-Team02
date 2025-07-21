using System.Dynamic;

#pragma warning disable CA1050 // Declare types in namespaces
public class FileReader
#pragma warning restore CA1050 // Declare types in namespaces
{
    // public string folderPath {get; set;}
    public static string[] ReadAllFileNames(string address) {
        string[] files = {};
        if (Directory.Exists(address)){
            files = Directory.GetFiles(address);
        }
        return files;
    }

    // static string ReadFile(string fileAddress) {
    //     string txt;
    //     try
    //     {
    //         txt = File.ReadAllText(fileAddress);
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.Error.WriteLine($"Error: file could not be opened. {ex.Message}");
    //     }
    //     return txt;
    // }
    
}
