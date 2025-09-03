using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers;
using MapperAI.Core.Mappers.Interfaces;

namespace MapperAI.Test;

public class PdfMapperIntegrationTests : BaseTests
{
    private readonly IPDFMapper _pdfMapper;

    public PdfMapperIntegrationTests()
    {
        var clientConfiguration = new MapperClientConfiguration("AIzaSyCiJxEdi-yzBg5GPsPhh6etWEKXzZATXtU", ModelType.GeminiFlash2_0);
        _pdfMapper = new PdfMapper(Serializer, Factory, clientConfiguration, new HttpClient());
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
        var pdfPath = Path.Combine("../../../Curriculo - Diego.pdf");

        var model = await _pdfMapper.MapAsync<CurriculumModel>(pdfPath);

        Assert.NotNull(model);
        Assert.Equal("Análise e desenvolvimento de sistemas EAD", model?.Curso);
        Assert.Equal("diegomagalhaesdev@gmail.com", model?.Email);
        Assert.True(model?.Projects.Count > 0);
    }
}

public class CurriculumModel
{
    public string Faculdade { get; set; }
    public string Curso { get; set; }
    public string Email { get; set; }
        
        
    public List<CurriculumProjects> Projects { get; set; }

}


public class CurriculumProjects
{
    public string Nome { get; set; }
    public List<string> Tecnologias { get; set; } = new List<string>();
}

public class CodigoPenal
{
    public string? Presidente { get; set;}
    public List<string> SuplentesDeSecretario { get; set; } = [];

}