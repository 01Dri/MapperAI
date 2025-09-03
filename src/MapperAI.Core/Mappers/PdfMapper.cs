using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using MapperAI.Core.Clients.Interfaces;
using MapperAI.Core.Clients.Models;
using MapperAI.Core.Extensions.Initializers;
using MapperAI.Core.Mappers.Interfaces;
using MapperAI.Core.Serializers.Interfaces;

namespace MapperAI.Core.Mappers;

public class PdfMapper : IPDFMapper
{
    private readonly IMapperSerializer _serializer;
    private readonly IMapperClientFactory _mapperClientFactory;
    private readonly MapperClientConfiguration _clientConfiguration;
    private readonly HttpClient? _httpClient;

    public PdfMapper(IMapperSerializer serializer, IMapperClientFactory mapperClientFactory, MapperClientConfiguration clientConfiguration, HttpClient? httpClient = null)
    {
        _serializer = serializer;
        _mapperClientFactory = mapperClientFactory;
        _clientConfiguration = clientConfiguration;
        _httpClient = httpClient;
    }

    public async Task<T?> MapAsync<T>(string pdfPath,  CancellationToken cancellationToken = default) where T : class, new()
    {
        var isWeb = IsWebLink(pdfPath);
        if (isWeb && _httpClient == null) throw new ArgumentException("HttpClient instance is required");
        var iai = _mapperClientFactory.CreateClient(_clientConfiguration);
        var pdfContent = isWeb ? await ExtractPdfWebContent(pdfPath) : SerializePdfContent(new PdfReader(pdfPath));
        var destinyObject = new T();
        destinyObject.Initialize();
        var prompt = CreatePrompt(pdfContent, _serializer.Serialize(destinyObject));
        var result = await iai.SendAsync(prompt, cancellationToken);
        return _serializer.Deserialize<T>(result.Value);
    }
    

    private static string CreatePrompt(string pdfContent, string classStructure)
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


    private async Task<string> ExtractPdfWebContent(string pdfUri)
    {
        if (pdfUri.StartsWith("https://drive.google.com") && !pdfUri.Contains("uc?export=download"))
            pdfUri = ParseDriveUrl(pdfUri);
        
        var requestResult = await _httpClient!.GetAsync(pdfUri);
        requestResult.EnsureSuccessStatusCode();
        var content = requestResult.Content;
        var stream = await content.ReadAsStreamAsync();
        var pdfReader = new PdfReader(stream);
        return SerializePdfContent(pdfReader);
    }

    private string SerializePdfContent(PdfReader reader)
    {
        var pdfDoc = new PdfDocument(reader);
        var extractedData = new List<string>();

        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
        {
            var page = pdfDoc.GetPage(i);
            var text = PdfTextExtractor.GetTextFromPage(page);
            var cleanedText = CleanText(text);

            extractedData.Add(cleanedText);
        }
        return _serializer.Serialize(extractedData);
    }

    private static string ParseDriveUrl(string pdfUri)
    {
        var uri = new Uri(pdfUri);

        var segments = uri.Segments;
        string? fileId = null;

        for (var i = 0; i < segments.Length; i++)
        {
            if (segments[i] != "d/" || i + 1 >= segments.Length) continue;
            fileId = segments[i + 1].TrimEnd('/');
            break;
        }
        if (string.IsNullOrEmpty(fileId)) throw new ArgumentException("Invalid drive link");
        return  $"https://drive.google.com/uc?export=download&id={fileId}";
    }
    private static string CleanText(string input)
    {
        return string.Join(" ", input.Split(['\n', '\r'],
                StringSplitOptions.RemoveEmptyEntries))
            .Replace("\\n", " ")
            .Replace("\\r", " ")
            .Trim();
    }

    private static bool IsWebLink(string pdfPath) => pdfPath.StartsWith("https://") || pdfPath.StartsWith("http://");
}