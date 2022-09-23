using System.Net;
using Confluent.Kafka;

var vmHost = "192.168.0.127:9092";
// vmHost = "255.255.252.0:9092";
var config = new ProducerConfig
{
    BootstrapServers = vmHost,
    ClientId = Dns.GetHostName(),
    Partitioner = Partitioner.Consistent,
    Acks = Acks.All,
    QueueBufferingMaxMessages = 10000000,
};

new Producer().SendStrings(config);


public class Producer
{
    private string Topic => "requestTopic";

    public void SendStrings(ProducerConfig producerConfig)
    {
        using var producer = new ProducerBuilder<int, string>(producerConfig).Build();
        using var producer2 = new ProducerBuilder<int, string>(producerConfig).Build();
        using var producer3 = new ProducerBuilder<int, string>(producerConfig).Build();
        using var producer4 = new ProducerBuilder<int, string>(producerConfig).Build();
        using var producer5 = new ProducerBuilder<int, string>(producerConfig).Build();


        for (int i = 0, l = 0, toFlush = 0; i < 100000; i++, l++, toFlush++)
        {
            var message = new Message<int, string> { Value = ElasticMock(i), Key = 1 };
            switch (l)
            {
                case 0:
                {
                    producer.Produce(Topic, message);
                    break;
                }

                case 1:
                {
                    producer2.Produce(Topic, message);
                    break;
                }

                case 2:
                {
                    producer3.Produce(Topic, message);
                    break;
                }

                case 3:
                {
                    producer4.Produce(Topic, message);
                    break;
                }

                case 4:
                {
                    producer5.Produce(Topic, message);
                    break;
                }
            }

            if (l >= 4) l = 0;
            if (toFlush >= 50)
            {
                producer.Flush();
                producer2.Flush();
                producer3.Flush();
                producer4.Flush();
                producer5.Flush();
                toFlush = 0;
            }
        }
    }

    private static string ElasticMock(int i) =>
        $"{{\"HostConfig\":{{\"Scheme\":\"https\",\"Host\":\"localhost\",\"Port\":9200}},\"RequestParameters\":{{\"Index\":\"test3\",\"Type\":\"_doc\",\"DocId\":null}},\"Data\":\"{{\\\"name\\\":\\\"New One - {i}\\\"}}\"}}";
}