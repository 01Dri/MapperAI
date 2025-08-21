using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.DI;
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Core.Serializers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MapperAI.Test.DI;

public class DependencyInjectionTests
{
    [Fact]
    public void AddMapperAI_ShouldRegisterServices()
    {
        var services = new ServiceCollection();

        // Act
        services.AddMapperAI("fake-key",  ModelType.GeminiFlash2_0);
        var provider = services.BuildServiceProvider();

        // Assert
        Assert.NotNull(provider.GetService<IMapperSerializer>());
        Assert.NotNull(provider.GetService<IMapperClientFactory>());
        Assert.NotNull(provider.GetService<IClassMapper>());
        Assert.NotNull(provider.GetService<IFileMapper>());
        Assert.NotNull(provider.GetService<IPDFMapper>());
    }
}