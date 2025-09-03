namespace MapperAI.Core.Mappers.Interfaces;

public interface IPDFMapper
{
    Task<T?> MapAsync<T>(string pdfPath, CancellationToken cancellationToken = default)
        where T : class, new();


}