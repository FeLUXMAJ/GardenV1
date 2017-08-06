using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using RdKafka;

using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace SimpleProducer
{
    class Program
    {
        static void Main(/*string[] args*/)
        {
            //string brokerList = args[0];
            //string topicName = args[1];

            //using (Producer producer = new Producer(brokerList))
            //using (Topic topic = producer.Topic(topicName))
            //{
            //    Console.WriteLine($"{producer.Name} producing on {topic.Name}. q to exit.");

            //    string text;
            //    while ((text = Console.ReadLine()) != "q")
            //    {
            //        byte[] data = Encoding.UTF8.GetBytes(text);
            //        Task<DeliveryReport> deliveryReport = topic.Produce(data);
            //        var unused = deliveryReport.ContinueWith(task =>
            //        {
            //            Console.WriteLine($"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}");
            //        });
            //    }
            //}

            //using (Producer producer = new Producer("127.0.0.1:9092"))
            //using (Topic topic = producer.Topic("test"))
            //{
            //    byte[] data = Encoding.UTF8.GetBytes("Hello RdKafka");                
            //    Task<DeliveryReport> deliveryReport = topic.Produce(data);
            //    var unused = deliveryReport.ContinueWith(task =>
            //    {
            //        Console.WriteLine($"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}");
            //    });
            //}
            //Console.ReadKey();

            string brokerList = "127.0.0.1:9092";
            string topicName = "test";

            var config = new Dictionary<string, object> { { "bootstrap.servers", brokerList } };

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                Console.WriteLine($"{producer.Name} producing on {topicName}. q to exit.");

                string text;
                while ((text = Console.ReadLine()) != "q")
                {
                    var deliveryReport = producer.ProduceAsync(topicName, null, text);
                    deliveryReport.ContinueWith(task =>
                    {
                        Console.WriteLine($"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}");
                    });
                }

                // Tasks are not waited on synchronously (ContinueWith is not synchronous),
                // so it's possible they may still in progress here.
                producer.Flush((int)TimeSpan.FromSeconds(10).TotalMilliseconds);
            }

        }
    }
}
