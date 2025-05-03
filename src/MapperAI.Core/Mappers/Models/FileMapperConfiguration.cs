namespace MapperAI.Core.Mappers.Models;

public class FileMapperConfiguration
{
    public string OutputFolder { get; set; }
    public string InputFolder { get; set; }
    public string? NameSpace { get; set; }
    public string Extension { get; set; } = "C#";
    
    public string? LanguageVersion { get; set; }
    public string? FileName { get; set; }
    public  bool IsUniqueClass => FileName != null; 



    public FileMapperConfiguration(string inputFolder, string outputFolder)
    {
        InputFolder = inputFolder;
        OutputFolder = outputFolder;
    }
}