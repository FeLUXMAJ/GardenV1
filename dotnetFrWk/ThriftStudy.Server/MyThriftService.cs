using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using ThriftStudy.Interface;
using ThriftStudy.Interface;

namespace ThriftStudy.Server
{
    public class MyThriftService : HelloService.Iface
    {
        /// <summary>
        /// 只有一个参数返回值为字符串类型的方法
        /// </summary>
        /// <param name="para">string类型参数</param>
        /// <returns>返回值为string类型</returns>
        public string HelloString(string para)
        {
            Console.WriteLine("客户端调用了HelloString方法");
            return para;
        }

        /// <summary>
        /// 只有一个参数，返回值为int类型的方法
        /// </summary>
        /// <param name="para"></param>
        /// <returns>返回值为int类型</returns>
        public int HelloInt(int para)
        {
            Console.WriteLine("客户端调用了HelloInt方法");
            return para;
        }

        /// <summary>
        /// 只有一个bool类型参数，返回值为bool类型的方法
        /// </summary>
        /// <param name="para"></param>
        /// <returns>返回值为bool类型</returns>
        public bool HelloBoolean(bool para)
        {
            Console.WriteLine("客户端调用了HelloBoolean方法");
            return para;
        }

        /// <summary>
        /// 返回执行为空的方法
        /// </summary>
        public void HelloVoid()
        {
            Console.WriteLine("客户端调用了HelloVoid方法");
            Console.WriteLine("HelloWorld");
        }

        /// <summary>
        /// 无参数，返回值为null的方法
        /// </summary>
        /// <returns>返回值为null</returns>
        public string HelloNull()
        {
            Console.WriteLine("客户端调用了HelloNull方法");
            return null;
        }
    }
}
