
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Core.Mappers.Models;
using MapperAI.Test.Utils.Helpers;
using MapperAI.Test.Utils.Mocks;

namespace MapperAI.Test.FileMapper.Gemini;

public class GeminiFileMapperTests : Utils.BaseTests
{
    private readonly IFileMapper _mapper;

    private string InputFolder => Path.Combine(FoldersHelpers.GetProjectDefaultPath(), "Utils", "Class");
    private string OutputFolder => Path.Combine(FoldersHelpers.GetProjectDefaultPath(), "MappedClasses");

    public GeminiFileMapperTests()
    {
        var clientConfiguration = MapperConfigurationMock.GetConfig(ModelType.Gemini);
        _mapper = new Core.Mappers.FileMapper(Factory, Serializer, clientConfiguration);

    }


    [Fact]
    public async Task Test_Should_Create_4_Files_With_Go_Extension()
    {

        FileMapperConfiguration configuration = new FileMapperConfiguration(InputFolder, OutputFolder)
        {
            NameSpace = "MapperAI.Test.MappedClasses",
            Extension = "go",
        };
        
        await _mapper.MapAsync(configuration);
        var files = Directory.GetFiles(OutputFolder).Where(x => x.Contains("go")).ToList();
        Assert.True(files.Count == 4);


    }
    
    [Fact]
    public async Task Test_Should_Create_4_Files_With_CSharp_Extension()
    {

        string extesionToMap = "php";
        FileMapperConfiguration configuration = new FileMapperConfiguration(InputFolder, OutputFolder)
        {
            NameSpace = "MapperAI.Test.MappedClasses",
            Extension = extesionToMap,
        };
        await _mapper.MapAsync(configuration);
        var files = Directory.GetFiles(OutputFolder);
        Assert.True(files.Length == 4);
        Assert.True(files.All(x => x.Contains($".{extesionToMap}")));


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
        await _mapper.MapAsync(configuration);
        var files = Directory.GetFiles(OutputFolder).Where(x => x.Contains(".cs")).ToList();
        Assert.True(files.Count == 1);

    }

    [Fact]
    public async Task Should_Create_1_File_With_CSharp_Extension_With_Mock()
    {
        // Arrange
        FileMapperConfiguration configuration = new FileMapperConfiguration(InputFolder, OutputFolder)
        {
            NameSpace = "MapperAI.Test.MappedClasses",
            Extension = "c#",
            FileName = "Carro.java",
            LanguageVersion = "13"
        };

        var expectedContent = new List<object> { new { Name = "Carro.cs", Content = "public class Carro { }" } };

        var clientConfiguration = MapperConfigurationMock.GetConfig(ModelType.Gemini);
        var mapperClientMock = FileMapperMocks.GetMapperClientMock(Serializer.Serialize(expectedContent));
        var mapperFactoryMock = FileMapperMocks.GetMapperClientFactoryMock(mapperClientMock.Object);
        var fileMapper = new Core.Mappers.FileMapper(mapperFactoryMock.Object, Serializer, clientConfiguration);

        // Act
        await fileMapper.MapAsync(configuration);

        // Assert
        var files = Directory.GetFiles(OutputFolder).Where(x => x.Contains(".cs")).ToList();
        Assert.True(files.Count == 1);
    }
}
