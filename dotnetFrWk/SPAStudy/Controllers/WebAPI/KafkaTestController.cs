using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Configuration;
using System.Text;
using System.Threading.Tasks;

using Confluent.Kafka;
using Confluent.Kafka.Serialization;

using SPAStudy.Common;

namespace SPAStudy.Controllers
{
    [RoutePrefix("api/KafkaTest")]
    public class KafkaTestController : ApiController
    {
        static string _conn =
            ConfigurationManager.AppSettings["RQKafka"] ?? "127.0.0.1:9092";
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sTopic">主题</param>
        /// <param name="sMsg">消息</param>
        /// <returns></returns>
        [HttpGet]
        public string SendMessage(string sTopic, string sMsg)
        {
            string str = string.Empty;

            try
            {
                string brokerList = _conn;
                string topicName = sTopic;

                var config = new Dictionary<string, object> { { "bootstrap.servers", brokerList } };

                using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
                {
                    var deliveryReport = producer.ProduceAsync(topicName, null, sMsg);

                    deliveryReport.ContinueWith(task =>
                    {
                        str = $"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}";
                        LogHelper.WriteLog(str);
                    });

                    // Tasks are not waited on synchronously (ContinueWith is not synchronous),
                    // so it's possible they may still in progress here.
                    producer.Flush((int)TimeSpan.FromSeconds(10).TotalMilliseconds);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e.ToString(), e);
            }

            return str;
        }

        //[HttpGet]
        //public void ReceiveMessage(string sTopic)
        //{
        //    try
        //    {
        //        string[] args = new string[] { _conn, sTopic };

        //        string brokerList = args[0];
        //        var topics = args.Skip(1).ToList();

        //        var config = new Dictionary<string, object>{
        //            { "group.id", "simple-csharp-consumer" },
        //            { "bootstrap.servers", brokerList }
        //        };

        //        using (var consumer = new Consumer<Null, string>(config, null,
        //            new StringDeserializer(Encoding.UTF8)))
        //        {
        //            consumer.Assign(new List<TopicPartitionOffset> {
        //            new TopicPartitionOffset(topics.First(), 0, 0) });

        //            //consumer.OnMessage += (obj, e) =>
        //            //{
        //            //    BroadcastMessage(e.Value);
        //            //    LogHelper.WriteLog(e.Value);
        //            //};

        //            while (true)
        //            {
        //                Message<Null, string> msg;
        //                if (consumer.Consume(out msg, TimeSpan.FromSeconds(1)))
        //                {
        //                    //BroadcastMessage(msg.Value);
        //                    LogHelper.WriteLog($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        LogHelper.WriteLog(e.ToString());
        //    }
        //}
    }
}
