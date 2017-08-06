using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Redis;

namespace redisDemo
{
    class Program
    {
        static RedisClient redisClient =
            new RedisClient("127.0.0.1", 6379);    //redis服务IP和端口
        static void Main(string[] args)
        {
            Console.WriteLine(redisClient.Get<string>("city"));
            Console.ReadKey();
        }
    }
}
