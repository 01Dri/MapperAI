
namespace MapperAI.Test.Helpers;

public class FoldersHelpers
{
    public static string GetProjectDefaultPath()
    {
        string defaultSolutionPath = AppDomain.CurrentDomain.BaseDirectory;
        return Directory.GetParent(defaultSolutionPath).Parent.Parent.Parent.FullName;
    }
    
}