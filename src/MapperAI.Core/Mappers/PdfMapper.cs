﻿using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Initializers;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Mappers;

public class PdfMapper : IPDFMapper
{
    private readonly IMapperSerializer _serializer;
    private readonly IMapperClientFactory _mapperClientFactory;
    private readonly MapperClientConfiguration _clientConfiguration;

    public PdfMapper(IMapperSerializer serializer, IMapperClientFactory mapperClientFactory, MapperClientConfiguration clientConfiguration)
    {
        _serializer = serializer;
        _mapperClientFactory = mapperClientFactory;
        _clientConfiguration = clientConfiguration;
    }


    public async Task<T?> MapAsync<T>(string pdfPath, CancellationToken cancellationToken = default) where T : class, new()
    {
        IMapperClient iai = _mapperClientFactory.CreateClient(_clientConfiguration);
        string pdfContent = ExtractPdfContent(pdfPath);
        T destinyObject = new T();
        DependencyInitializer.Initialize(destinyObject);
        string prompt = CreatePrompt(pdfContent, _serializer.Serialize(destinyObject));
        MapperClientResponse result = await iai.SendAsync(prompt, cancellationToken);
        return _serializer.Deserialize<T>(result.Value);
    }

    private string CreatePrompt(string pdfContent, string classStructure)
    {
        return $"""
                You are a senior software engineer specializing in data extraction and mapping.

                I will give you the content of a PDF document and a C# class structure.
                Your task is to extract the relevant information from the PDF and populate the properties of the class with accurate values.

                ### PDF Content:
                {pdfContent}

                ### C# Class Structure:
                {classStructure}

                Return ONLY a valid JSON object that matches the C# class structure and can be deserialized directly.

                Do not include explanations or markdown formatting.
                """;
    }


    private string ExtractPdfContent(string pdfPath)
    {
        var pdfReader = new PdfReader(pdfPath);
        var pdfDoc = new PdfDocument(pdfReader);
        var extractedData = new List<string>();
    
        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
        {
            var page = pdfDoc.GetPage(i);
            string text = PdfTextExtractor.GetTextFromPage(page);
            string cleanedText = CleanText(text);
    
            extractedData.Add(cleanedText);
        }

        return _serializer.Serialize(extractedData);

    }
    
    private string CleanText(string input)
    {
        return string.Join(" ", input.Split(new[] { '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries))
            .Replace("\\n", " ")
            .Replace("\\r", " ")
            .Trim();
    }
        
}