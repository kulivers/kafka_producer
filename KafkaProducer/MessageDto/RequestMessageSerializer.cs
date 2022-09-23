using System.Text;
using Comindware.Gateway;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace MessageBus;

public class MessageDtoSerializer : ISerializer<IMessageDto>
{
    public byte[] Serialize(IMessageDto data, SerializationContext context)
    {
        var serialized = JsonConvert.SerializeObject(data);
        var bytes = Encoding.ASCII.GetBytes(serialized);
        return bytes;
    }
}