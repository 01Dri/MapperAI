
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using Moq;

namespace MapperAI.Test.Utils.Mocks;

public static class PdfMapperMocks
{
    public static Mock<IMapperClient> GetMapperClientMock(string expected)
    {
        var mock = new Mock<IMapperClient>();
        mock.Setup(x => x.SendAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new MapperClientResponse(expected));
        return mock;
    }
    
    public static Mock<IMapperClientFactory> GetMapperClientFactoryMock(IMapperClient client)
    {
        var mock = new Mock<IMapperClientFactory>();
        mock.Setup(x => x.CreateClient(It.IsAny<MapperClientConfiguration>()))
            .Returns(client);
        return mock;
    }
}
