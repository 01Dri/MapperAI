using MapperAI.Core.Clients;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Core.Serializers;

namespace MapperAI.Test;

public class ClassMapperTests
{
    private readonly IClassMapper _classClassMapper = new ClassMapper(new MapperSerializer(), new ClientFactoryAI(new MapperSerializer()));
    private readonly ClientConfiguration _clientConfiguration = new ()
    {
        Type = ModelType.Ollama,
        Model = "deepseek-r1",
        ApiKey = Environment.GetEnvironmentVariable("GEMINI_KEY")
    };
    
    
    [Fact]
    public async Task Test1()
    {
        var person = new Person()
        {
            Id = Guid.NewGuid(),
            Name = "Diego"
        };

        var result = await _classClassMapper.MapAsync<Person, PersonDTO>(person, _clientConfiguration);
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