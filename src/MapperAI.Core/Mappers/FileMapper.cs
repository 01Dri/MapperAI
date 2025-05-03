using System.Runtime.Serialization;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Core.Mappers.Models;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Mappers;

public class FileMapper : IFileMapper
{

    private readonly IMapperClientFactory _mapperClientFactory;
    private readonly IMapperSerializer _serializer;

    public FileMapper(IMapperClientFactory mapperClientFactory, IMapperSerializer serializer)
    {
        _mapperClientFactory = mapperClientFactory;
        _serializer = serializer;
    }

    public async Task MapAsync(FileMapperConfiguration configuration, MapperClientConfiguration mapperClientConfiguration,
        CancellationToken cancellationToken = default)
    {
        IMapperClient mapperClient = _mapperClientFactory.CreateClient(mapperClientConfiguration);
        List<MapperClassContent> filesToMap = GetFilesToMap(configuration);
        string prompt = CreatePrompt(filesToMap, configuration);
        MapperClientResponse result = await mapperClient.SendAsync(prompt, cancellationToken);
        List<MapperClassContent>? contents = _serializer.Deserialize<List<MapperClassContent>>(result.Value);
        if (contents != null && contents.Any())
        {
            CreateFiles(contents, configuration.OutputFolder);
            return;
        }
        throw new SerializationException("Failed to serialize contents");
    }

    private void CreateFiles(List<MapperClassContent> contents, string outputFolderPath)
    {
         if (!Directory.Exists(outputFolderPath))
        {
            Directory.CreateDirectory(outputFolderPath);
        }

        contents.ForEach(x =>
        {
            var filePath = Path.Combine(outputFolderPath, x.Name);
            File.WriteAllText(filePath, x.Content);
        });
    }

    private string CreatePrompt(List<MapperClassContent> filesToMap, FileMapperConfiguration configuration)
    {
        var serializedFiles = _serializer.Serialize(filesToMap);

        var namespaceNote = string.IsNullOrWhiteSpace(configuration.NameSpace)
            ? "No namespace is provided. Omit namespace declarations in the result."
            : $"Use the namespace: \"{configuration.NameSpace}\" for all converted classes.";

        var languageVersionNote = string.IsNullOrWhiteSpace(configuration.LanguageVersion)
            ? ""
            : $" Use all best practices and features available in {configuration.Extension} version {configuration.LanguageVersion}.";

        return
            $@"
            You are an expert code converter AI. Your task is to convert each of the following source code files into valid {configuration.Extension} classes, preserving all individual files and their contents.

            Input format:
            A JSON array of objects, where each object has the structure:
            [
              {{
                ""Name"": ""<OriginalFileName>"",
                ""Content"": ""<OriginalSourceCode>""
              }},
              ...
            ]

            You must:
            - Convert **each** object in the list individually.
            - Return a **JSON array** of converted files, **one for each input file**.
            - The output must have the **same number of items** as the input.
            - Each converted object must follow this structure:
              {{
                ""Name"": ""<ConvertedFileName>"",
                ""Content"": ""<ConvertedSourceCode>""
              }}

            Rules:
            - `Name`: Convert to PascalCase and use the correct file extension for {configuration.Extension}.
            - `Content`: Must be valid, complete code in {configuration.Extension}.{languageVersionNote}
            - {namespaceNote}
            - Follow **all best practices, conventions, and idiomatic usage** of {configuration.Extension}.
            - Use clean code principles: clear naming, proper formatting, consistent style, and modular design.
            - Avoid deprecated patterns or anti-patterns.
            - **Escape all backslashes properly for JSON**: every single `\` must become `\\` to ensure valid JSON strings (especially in paths, regexes, or PHP variables like `\$`).
            - **DO NOT** return markdown, explanations, comments, or any text outside of the JSON array.
            - Your response **must be directly deserializable** into List<ClassContent>.

            Input Files:
            {serializedFiles}
            ";
        }

    private List<MapperClassContent> GetFilesToMap(FileMapperConfiguration configuration)
    {
        string path;
        if (configuration.IsUniqueClass)
        {
            if (configuration.FileName == null) throw new ArgumentException("FileName is required!");
            path = Path.Combine(configuration.InputFolder, configuration.FileName);
            var content = new MapperClassContent()
            {
                Name = Path.GetFileName(configuration.FileName),
                Content = File.ReadAllText(path)
            };
            return new List<MapperClassContent>() { content };
        }

        return Directory.GetFiles(configuration.InputFolder)
            .Where(file => file.Contains("."))
            .Select(x => new MapperClassContent()
            {
                Name = Path.GetFileName(x),
                Content = File.ReadAllText(x)
            }).ToList();
        
        
    }
}