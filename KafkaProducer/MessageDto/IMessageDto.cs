using System.Collections.Generic;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;

namespace Comindware.Gateway;

public interface IMessageDto
{
    string RequestMethod { get; }
    string Scheme { get; }
    PathString Path { get; }
    string? Body { get; }
    Dictionary<string, string[]> Headers { get; }
    public Message<Null, IMessageDto> ToKafkaMessage(IMessageDto messageDto,
        Timestamp timestamp = default,
        Headers? hearder = null);
}