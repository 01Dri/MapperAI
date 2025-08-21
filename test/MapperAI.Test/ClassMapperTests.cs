using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers;
using MapperAI.Core.Mappers.Interfaces;

namespace MapperAI.Test;

public class ClassMapperTests : BaseTests
{
    private readonly IClassMapper _classMapper;

    public ClassMapperTests()
    {
        var clientConfiguration = new MapperClientConfiguration(Environment.GetEnvironmentVariable("GEMINI_KEY"),ModelType.GeminiFlash2_0);
        _classMapper = new ClassMapper(Serializer, Factory, clientConfiguration);
        
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
}

class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

class PersonDTO
{
    public string Name { get; set; }
}