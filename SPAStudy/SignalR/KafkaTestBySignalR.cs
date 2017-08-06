using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Text;

using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

using SPAStudy.Common;

namespace SPAStudy.SignalR
{
    /// <summary>
    /// 
    /// </summary>
    public class KafkaTestBySignalR
    {
        // Singleton instance
        private readonly static Lazy<KafkaTestBySignalR>
            _instance = new Lazy<KafkaTestBySignalR>(
                () => new KafkaTestBySignalR(GlobalHost.ConnectionManager
                .GetHubContext<KafkaTestBySignalRHub>().Clients));

        private readonly object _msgMonitorStateLock = new object();
        private volatile MessageMonitorState _msgMonitorState;

        private KafkaTestBySignalR(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public static KafkaTestBySignalR Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public MessageMonitorState MessageMonitorState
        {
            get { return _msgMonitorState; }
            private set { _msgMonitorState = value; }
        }

        static string _conn
            = ConfigurationManager.AppSettings["RQKafka"] ?? "127.0.0.1:9092";

        /// <summary>
        /// 启动消息监听
        /// </summary>
        public void StartMonitorKafkaMessage(string sTopic)
        {
            try
            {
                lock (_msgMonitorStateLock)
                {
                    if (_msgMonitorState != MessageMonitorState.Open)
                    {
                        _msgMonitorState = MessageMonitorState.Open;
                        BroadcastMessageMonitorState();

                        string[] args = new string[] { _conn, sTopic };

                        string brokerList = args[0];
                        var topics = args.Skip(1).ToList();

                        var config = new Dictionary<string, object>{
                            { "group.id", "simple-csharp-consumer" },
                            { "bootstrap.servers", brokerList }};

                        using (var consumer = new Consumer<Null, string>(
                            config, null, new StringDeserializer(Encoding.UTF8)))
                        {
                            consumer.Assign(new List<TopicPartitionOffset> {
                            new TopicPartitionOffset(topics.First(), 0, 0) });

                            while (_msgMonitorState == MessageMonitorState.Open)
                            {
                                Message<Null, string> msg;
                                if (consumer.Consume(out msg, TimeSpan.FromSeconds(1)))
                                {
                                    BroadcastKafkaMessage(msg.Value);
                                    LogHelper.WriteLog($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e.ToString());
            }
        }

        /// <summary>
        /// 停止消息监听
        /// </summary>
        public void StopMonitorKafkaMessage()
        {
            lock (_msgMonitorStateLock)
            {
                _msgMonitorState = MessageMonitorState.Closed;
                BroadcastMessageMonitorState();
            }
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="sMsg"></param>
        private void BroadcastKafkaMessage(string sMsg)
        {
            Clients.All.ReceiveKafkaMessage(sMsg);
        }

        /// <summary>
        /// 广播消息监听器的状态
        /// </summary>
        /// <param name="sMsg"></param>
        private void BroadcastMessageMonitorState()
        {
            Clients.All.MessageMonitorState(_msgMonitorState);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [HubName("KafkaTestBySignalR")]
    public class KafkaTestBySignalRHub : Hub
    {
        private readonly KafkaTestBySignalR _KafkaTest;

        public KafkaTestBySignalRHub() :
            this(KafkaTestBySignalR.Instance)
        {
        }

        public KafkaTestBySignalRHub(KafkaTestBySignalR KafkaTest)
        {
            _KafkaTest = KafkaTest;
        }

        public void StartMonitorKafkaMessage(string sTopic)
        {
            _KafkaTest.StartMonitorKafkaMessage(sTopic);
        }

        public void StopMonitorKafkaMessage()
        {
            _KafkaTest.StopMonitorKafkaMessage();
        }
    }

    /// <summary>
    /// 消息监听状态
    /// </summary>
    public enum MessageMonitorState
    {
        Closed,
        Open
    }
}