using MapperAI.Core.Clients;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Enums;
using MapperAI.Core.Mappers;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Core.Serializers;
using MapperAI.Core.Serializers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MapperAI.Core.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddMapperAI(this IServiceCollection services, string apiKey, ModelType modelType)
    {
        services.AddMapperClientConfiguration(apiKey, modelType);
        services.AddHttpClient();
        services.AddSingleton<IMapperSerializer, MapperSerializer>();
        services.AddSingleton<IMapperClientFactory, MapperClientFactory>();
        services.AddScoped<IClassMapper, ClassMapper>();
        
        services.AddScoped<IClassMapper, ClassMapper>();
        services.AddScoped<IFileMapper, FileMapper>();
        services.AddScoped<IPDFMapper, PdfMapper>();
        
        return services;
    }

    private static void AddMapperClientConfiguration(this IServiceCollection services, string apiKey, ModelType modelType)
    {
        MapperClientConfiguration configuration = new(apiKey, modelType, "");
        services.AddSingleton<MapperClientConfiguration>(s => configuration);

    }
}