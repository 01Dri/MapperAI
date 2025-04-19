using MapperIA.Core.Helpers;

namespace MapperIA.Core.Mappers.Models;

public class FileMapperConfiguration
{
    public string OutputFolder { get; set; } = FoldersHelpers.GetProjectDefaultPath();
    public string InputFolder { get; set; } = "Class";
    public string? NameSpaceValue { get; set; }
    public string ProjectName { get; }
    public bool IsUniqueClass { get;  }
    public string? FileName { get; }
    
    public FileMapperConfiguration(bool isUniqueClass, string projectName, string? fileName = null)
    {
        IsUniqueClass = isUniqueClass;
        ProjectName = projectName;
        FileName = fileName;
        IsUniqueClass = isUniqueClass;
        if (IsUniqueClass && string.IsNullOrEmpty(FileName)) throw new ArgumentException("FileName is required");
        if (string.IsNullOrEmpty(projectName)) throw new ArgumentException("ProjectName is required");
    }

}