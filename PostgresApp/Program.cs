using Core.nKafkaConnector;
using PostgresApp.nKafkaListener;
using System.Text;

namespace PostgresApp
{
    class Program
    {
        static void Main(string[] args)
        {
            cKafkaListener __KafkaListener = new cKafkaListener();

            //cKafkaConnector __KafkaConnector = new cKafkaConnector("127.0.0.1:9092", "eventlog");
            cKafkaConnector __KafkaConnector = new cKafkaConnector("host.docker.internal:29092", "topic");
            
            __KafkaConnector.Consumer.StartListener("Postgres", __KafkaListener);

            Console.WriteLine("Postgres App Started");
            string __Code = Console.ReadLine();

            while (__Code != "exit")
            {
                __Code = Console.ReadLine();
            }

            __KafkaConnector.Consumer.Stop();
        }
    }
}