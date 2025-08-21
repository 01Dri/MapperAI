using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers;
using MapperAI.Core.Mappers.Interfaces;

namespace MapperAI.Test;

public class PdfMapperTests : BaseTests
{
    private readonly IPDFMapper _pdfMapper;

    public PdfMapperTests()
    {
        var clientConfiguration = new MapperClientConfiguration( Environment.GetEnvironmentVariable("GEMINI_KEY"),ModelType.GeminiFlash2_0);
        _pdfMapper = new PdfMapper(Serializer, Factory, clientConfiguration);
    }

    [Fact]
    public async Task Test1()
    {
        var pdfPath = Path.Combine(@"../../../Curriculo - Diego.pdf");
        CurriculumModel? curriculumModel =  await _pdfMapper.MapAsync<CurriculumModel>(pdfPath);
        Assert.Contains("Uninter", curriculumModel?.Faculdade);
        Assert.Equal("Análise e desenvolvimento de sistemas EAD", curriculumModel?.Curso);
        Assert.Equal(2, curriculumModel?.Projects.Count);
        Assert.Equal("diegomagalhaesdev@gmail.com", curriculumModel?.Email);
        
        var expectedProjectNames = new List<string> { "ReclameTrancoso", "VTHoftalon" };
        var actualProjectNames = curriculumModel?.Projects.Select(p => p.Nome).ToList();

        Assert.Equal(expectedProjectNames, actualProjectNames);

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