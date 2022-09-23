using System.Collections.Generic;
using Comindware.Gateway;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;


namespace MessageBus;

public interface IMessageBusProducer
{
    public Task Produce(string topicName, Message<Null, IMessageDto> message);
}
