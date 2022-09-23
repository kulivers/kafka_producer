using Confluent.Kafka;

namespace FastScratchConsole;

public class RequestMessageSerializer : ISerializer<HttpRequestMessage>
{
    public byte[] Serialize(HttpRequestMessage request, SerializationContext context) =>
        new HttpMessageContent(request).ReadAsByteArrayAsync().Result;
}