﻿using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Core.Mappers.Models;
using MapperAI.Test.Helpers;

namespace MapperAI.Test;

public class FileMapperTests : BaseTests
{

    private readonly MapperClientConfiguration _clientConfiguration;
    private readonly IFileMapper _mapper;

    private string InputFolder => Path.Combine(FoldersHelpers.GetProjectDefaultPath(), "Class");
    private string OutputFolder => Path.Combine(FoldersHelpers.GetProjectDefaultPath(), "MappedClasses");

    public FileMapperTests()
    {
        _clientConfiguration = new MapperClientConfiguration("gemini-2.0-flash", Environment.GetEnvironmentVariable("GEMINI_KEY"),ModelType.Gemini);
        _mapper = new FileMapper(Factory, Serializer, _clientConfiguration);

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
        var files = Directory.GetFiles(OutputFolder);
        Assert.True(files.Length == 4);
        Assert.True(files.All(x => x.Contains(".go")));


    }
    
    [Fact]
    public async Task Test_Should_Create_4_Files_With_CSharp_Extension()
    {

        string extesionToMap = "js";
        FileMapperConfiguration configuration = new FileMapperConfiguration(InputFolder, OutputFolder)
        {
            NameSpace = "MapperAI.Test.MappedClasses",
            Extension = extesionToMap
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
        var files = Directory.GetFiles(OutputFolder);
        Assert.True(files.Length == 1);
        Assert.True(files.All(x => x.Contains(".cs")));


    }
}