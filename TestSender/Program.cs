﻿using Core.nKafkaConnector;
using System.Text;

namespace MicroServiceSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sender Started");

            cKafkaConnector __KafkaConnector = new cKafkaConnector("127.0.0.1:9092", "topic");
            __KafkaConnector.Producer.Init();

            Console.Write("Message : ");
            string __Message = Console.ReadLine();

            while (__Message != "exit")
            {
                __KafkaConnector.Producer.Produce(__Message);
                Console.Write("Message : ");
                __Message = Console.ReadLine();
            }
        }
    }
}