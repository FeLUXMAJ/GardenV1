using System;

using Garden.Frame.Cache;

namespace RedisStudy
{
    class Program
    {
        static RedisHelper rh = new RedisHelper("127.0.0.1:6379");

        static void Main(string[] args)
        {
            Console.WriteLine(StringGet("city"));
            Console.ReadKey();
        }

        /// <summary>
        /// 根据Key获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string StringGet(string key)
        {
            return rh.StringGet(key);
        }

        /// <summary>
        /// 单条存值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool StringSet(string key, string value)
        {
            return rh.StringSet(key, value);
        }
    }
}