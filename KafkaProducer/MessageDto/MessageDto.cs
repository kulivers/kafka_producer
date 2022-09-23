using System.Collections.Generic;
using System.IO;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;

namespace Comindware.Gateway;


public record MessageDto : IMessageDto
{
    public string RequestMethod { get; }
    public string Scheme { get; }
    public PathString Path { get; }
    public string? Body { get; }
    public Dictionary<string, string[]> Headers { get; }
    public MessageDto(HttpRequest request, string body)
    {
        Body = body;
        Headers = ReadHeaders(request);
        RequestMethod = request.Method;
        Scheme = request.Scheme;
        Path = request.Path;
    }
    private Dictionary<string, string[]> ReadHeaders(HttpRequest request)
    {
        var headers = new Dictionary<string, string[]>();
        foreach (var (key, value) in request.Headers)
        {
            headers.Add(key, value);
        }

        return headers;
    }
    
    public Message<Null, IMessageDto> ToKafkaMessage(IMessageDto messageDto, Timestamp timestamp = default,
        Headers? hearder = null) =>
        new()
        {
            Value = messageDto,
            Headers = hearder,
            Timestamp = timestamp
        };
}