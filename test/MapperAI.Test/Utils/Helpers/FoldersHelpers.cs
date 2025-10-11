
namespace MapperAI.Test.Utils.Helpers;

public class FoldersHelpers
{
    public static string GetProjectDefaultPath()
    {
        string baseDirectory = AppContext.BaseDirectory;
        DirectoryInfo? directory = new DirectoryInfo(baseDirectory);
        while (directory != null && !directory.GetFiles("*.csproj").Any())
        {
            directory = directory.Parent;
        }
        return directory?.FullName ?? string.Empty;
    }
    
}