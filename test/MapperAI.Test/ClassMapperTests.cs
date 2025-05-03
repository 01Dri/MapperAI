using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers;
using MapperAI.Core.Mappers.Interfaces;

namespace MapperAI.Test;

public class ClassMapperTests : BaseTests
{
    private readonly IClassMapper _classMapper;
    private readonly MapperClientConfiguration _clientConfiguration;

    public ClassMapperTests()
    {
        _clientConfiguration = new MapperClientConfiguration("gemini-2.0-flash", Environment.GetEnvironmentVariable("GEMINI_KEY"),ModelType.Gemini);
        _classMapper = new ClassMapper(Serializer, Factory, _clientConfiguration);
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