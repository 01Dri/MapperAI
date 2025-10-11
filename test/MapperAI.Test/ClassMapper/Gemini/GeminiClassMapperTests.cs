
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Test.Utils.Mocks;

namespace MapperAI.Test.ClassMapper.Gemini;

public class GeminiClassMapperTests : Utils.BaseTests
{
    private readonly IClassMapper _classMapper;

    public GeminiClassMapperTests()
    {
        var clientConfiguration = MapperConfigurationMock.GetConfig(ModelType.Gemini);
        _classMapper = new Core.Mappers.ClassMapper(Serializer, Factory, clientConfiguration);
    }

    [Fact]
    public async Task Test1()
    {
        var person = new Person()
        {
            Id = Guid.NewGuid(),
            Name = "Diego"
        };

        var result = await _classMapper.MapAsync<Person, PersonDTO>(person);
        Assert.Equal(person.Name, result?.Name);
    }

    [Fact]
    public async Task TestGenericClient()
    {
        var person = new Person()
        {
            Id = Guid.NewGuid(),
            Name = "Diego"
        };
        
        var result = await _classMapper.MapAsync<Person, PersonDTO>(person);
        Assert.Equal(person.Name, result?.Name);
    }

    [Fact]
    public async Task Should_Map_Person_To_PersonDTO_With_Mock()
    {
        // Arrange
        var person = new Person
        {
            Id = Guid.NewGuid(),
            Name = "Diego"
        };

        var expectedDto = new PersonDTO
        {
            Name = person.Name
        };

        var clientConfiguration = MapperConfigurationMock.GetConfig(ModelType.Gemini);
        var mapperClientMock = ClassMapperMocks.GetMapperClientMock(Serializer.Serialize(expectedDto));
        var mapperFactoryMock = ClassMapperMocks.GetMapperClientFactoryMock(mapperClientMock.Object);
        var classMapper = new Core.Mappers.ClassMapper(Serializer, mapperFactoryMock.Object, clientConfiguration);

        // Act
        var result = await classMapper.MapAsync<Person, PersonDTO>(person);

        // Assert
        Assert.Equal(expectedDto.Name, result?.Name);
    }
}

class Person
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

class PersonDTO
{
    public string? Name { get; set; }
}
