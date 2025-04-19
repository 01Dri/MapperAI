using MapperAI.Core.Clients;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Helpers;
using MapperAI.Core.Mappers;
using MapperAI.Core.Mappers.Models;
using MapperAI.Core.Serializers;

namespace MapperAI.Test;

public class FileMapperTests
{
    private readonly FileMapper _mapper = new FileMapper(new ClientFactoryAI(new MapperSerializer()), new MapperSerializer());

    private readonly ClientConfiguration _clientConfiguration = new()
    {
        Type = ModelType.Gemini,
        Model = "gemini-2.0-flash",
        ApiKey = Environment.GetEnvironmentVariable("GEMINI_KEY")
    };

    private string InputFolder => Path.Combine(FoldersHelpers.GetProjectDefaultPath(), "Class");
    private string OutputFolder => Path.Combine(FoldersHelpers.GetProjectDefaultPath(), "MappedClasses");


    [Fact]
    public async Task Test_Should_Create_4_Files_With_Go_Extension()
    {

        FileMapperConfiguration configuration = new FileMapperConfiguration(InputFolder, OutputFolder)
        {
            NameSpace = "MapperAI.Test.MappedClasses",
            Extension = "go"
        };
        await _mapper.MapAsync(configuration, _clientConfiguration);
        var files = Directory.GetFiles(OutputFolder);
        Assert.True(files.Length == 4);
        Assert.True(files.All(x => x.Contains(".go")));


    }
    
    [Fact]
    public async Task Test_Should_Create_4_Files_With_CSharp_Extension()
    {

        FileMapperConfiguration configuration = new FileMapperConfiguration(InputFolder, OutputFolder)
        {
            NameSpace = "MapperAI.Test.MappedClasses",
            Extension = "c#"
        };
        await _mapper.MapAsync(configuration, _clientConfiguration);
        var files = Directory.GetFiles(OutputFolder);
        Assert.True(files.Length == 4);
        Assert.True(files.All(x => x.Contains(".cs")));


    }
    
    [Fact]
    public async Task Test_Should_Create_1_File_With_CSharp_Extension()
    {

        FileMapperConfiguration configuration = new FileMapperConfiguration(InputFolder, OutputFolder)
        {
            NameSpace = "MapperAI.Test.MappedClasses",
            Extension = "c#",
            FileName = "Carro.java",
            LanguageVersion = "13"
        };
        await _mapper.MapAsync(configuration, _clientConfiguration);
        var files = Directory.GetFiles(OutputFolder);
        Assert.True(files.Length == 1);
        Assert.True(files.All(x => x.Contains(".cs")));


    }
}