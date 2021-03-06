﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ThriftStudy.Interface;
using Thrift.Protocol;
using Thrift.Transport;

namespace ThriftStudy.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //设置服务端端口号和地址
                TTransport transport = new TSocket("localhost", 8085);
                transport.Open();
                //设置传输协议为二进制传输协议
                TProtocol protocol = new TBinaryProtocol(transport);
                //创建客户端对象
                HelloService.Client client = new HelloService.Client(protocol);
                //调用服务端的方法
                Console.WriteLine(client.HelloInt(123));
                Console.ReadKey();
            }
            catch (TTransportException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
