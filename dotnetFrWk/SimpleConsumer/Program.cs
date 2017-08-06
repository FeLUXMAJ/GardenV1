using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using RdKafka;

using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace SimpleConsumer
{
    class Program
    {
        static void Main(/*string[] args*/)
        {
            //string brokerList = args[0];
            //var topics = args.Skip(1).ToList();

            //var config = new Config() { GroupId = "simple-csharp-consumer" };
            //using (var consumer = new EventConsumer(config, brokerList))
            //{
            //    consumer.OnMessage += (obj, msg) =>
            //    {
            //        string text = Encoding.UTF8.GetString(msg.Payload, 0, msg.Payload.Length);
            //        Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {text}");
            //    };

            //    consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topics.First(), 0, 5) });
            //    consumer.Start();

            //    Console.WriteLine("Started consumer, press enter to stop consuming");
            //    Console.ReadLine();
            //}

            //var config = new Config() { GroupId = "example-csharp-consumer" };
            //using (var consumer = new EventConsumer(config, "127.0.0.1:9092"))
            //{
            //    consumer.OnMessage += (obj, msg) =>
            //    {
            //        string text = Encoding.UTF8.GetString(msg.Payload, 0, msg.Payload.Length);
            //        Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {text}");
            //    };

            //    List<string> ls = new List<string>();
            //    ls.Add("test");

            //    consumer.Subscribe(ls);
            //    consumer.Start();

            //    Console.WriteLine("Started consumer, press enter to stop consuming");
            //    Console.ReadLine();
            //}
            //Console.ReadKey();

            string[] args = new string[] { "127.0.0.1:9092", "test" };

            string brokerList = args[0];
            var topics = args.Skip(1).ToList();

            //var topics = args.Skip(1).ToList();
            //string brokerList = "127.0.0.1:9092";

            var config = new Dictionary<string, object>
            {
                { "group.id", "simple-csharp-consumer" },
                { "bootstrap.servers", brokerList }
            };

            using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
            {
                consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topics.First(), 0, 0) });

                while (true)
                {
                    Message<Null, string> msg;
                    if (consumer.Consume(out msg, TimeSpan.FromSeconds(1)))
                    {
                        Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
                    }
                }
            }
        }
    }
}
