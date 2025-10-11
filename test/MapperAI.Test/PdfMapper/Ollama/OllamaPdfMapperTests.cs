
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Test.Utils.Mocks;
using MapperAI.Test.Utils.Helpers;

namespace MapperAI.Test.PdfMapper.Ollama;

public class OllamaPdfMapperTests : Utils.BaseTests
{
    private readonly IPDFMapper _pdfMapper;

    public OllamaPdfMapperTests()
    {
        var clientConfiguration = MapperConfigurationMock.GetConfig(ModelType.Ollama);
        _pdfMapper = new Core.Mappers.PdfMapper(Serializer, Factory, clientConfiguration, new HttpClient());
    }

    [Fact]
    public async Task Should_Map_Curriculum_From_GoogleDrive()
    {
        var model = await _pdfMapper.MapAsync<CurriculumModel>(
            "https://drive.google.com/file/d/1ByhxqDtlX2d_jnmxF8kqgPZJNKs54k4R/view?usp=drive_link");

        Assert.NotNull(model);
        Assert.Equal("Análise e desenvolvimento de sistemas EAD", model?.Curso);
        Assert.Contains("Uninter", model?.Faculdade);
        Assert.Equal("diegomagalhaesdev@gmail.com", model?.Email);
    }

    [Fact]
    public async Task Should_Map_PenalCode_From_Senado()
    {
        var model = await _pdfMapper.MapAsync<CodigoPenal>(
            "https://www2.senado.leg.br/bdsf/bitstream/handle/id/529748/codigo_penal_1ed.pdf");

        Assert.NotNull(model);
        Assert.Equal("Senador Eunício Oliveira", model?.Presidente);
        Assert.Equal(4, model?.SuplentesDeSecretario.Count);
        Assert.Contains("Senador Eduardo Amorim", model?.SuplentesDeSecretario);
    }

    [Fact]
    public async Task Should_Map_Curriculum_From_LocalFile()
    {
        var pdfPath = Path.Combine(FoldersHelpers.GetProjectDefaultPath(), "Utils", "Curriculo - Diego.pdf");

        var model = await _pdfMapper.MapAsync<CurriculumModel>(pdfPath);

        Assert.NotNull(model);
        Assert.Equal("Análise e desenvolvimento de sistemas EAD", model?.Curso);
        Assert.Equal("diegomagalhaesdev@gmail.com", model?.Email);
        Assert.True(model?.Projects.Count > 0);
    }

    [Fact]
    public async Task Should_Map_Curriculum_From_LocalFile_With_Mock()
    {
        // Arrange
        var pdfPath = Path.Combine(FoldersHelpers.GetProjectDefaultPath(), "test", "MapperAI.Test", "Utils", "Curriculo - Diego.pdf");
        var expectedModel = new CurriculumModel
        {
            Curso = "Ciencia da computacao",
            Email = "test@test.com",
            Faculdade = "Uninter",
            Projects = new List<CurriculumProjects>
            {
                new()
                {
                    Nome = "MapperAI",
                    Tecnologias = new List<string> { "C#", ".NET" }
                }
            }
        };
        var clientConfiguration = MapperConfigurationMock.GetConfig(ModelType.Ollama);

        var mapperClientMock = PdfMapperMocks.GetMapperClientMock(Serializer.Serialize(expectedModel));
        var mapperFactoryMock = PdfMapperMocks.GetMapperClientFactoryMock(mapperClientMock.Object);
        var pdfMapper = new Core.Mappers.PdfMapper(Serializer, mapperFactoryMock.Object, clientConfiguration);

        // Act
        var model = await pdfMapper.MapAsync<CurriculumModel>(pdfPath);

        // Assert
        Assert.NotNull(model);
        Assert.Equal(expectedModel.Curso, model.Curso);
        Assert.Equal(expectedModel.Email, model.Email);
        Assert.Equal(expectedModel.Faculdade, model.Faculdade);
        Assert.Equal(expectedModel.Projects.Count, model.Projects.Count);
    }
}

public class CurriculumModel
{
    public string? Faculdade { get; set; }
    public string? Curso { get; set; }
    public string? Email { get; set; }
        
        
    public List<CurriculumProjects>? Projects { get; set; }

}


public class CurriculumProjects
{
    public string? Nome { get; set; }
    public List<string> Tecnologias { get; set; } = new List<string>();
}

public class CodigoPenal
{
    public string? Presidente { get; set;}
    public List<string>? SuplentesDeSecretario { get; set; } = [];

}
