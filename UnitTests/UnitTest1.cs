using Core.nDTOs.nEvent.nEventItem;
using Core.nKafkaConnector;
using MicroServiceSample.nWebGraph;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Nodes;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1 : IMessageReceiver
    {
        static cKafkaConnector KafkaConnectorSender { get; set; }

        static cKafkaConnector KafkaConnectorReceiever { get; set; }
        static string KAFKA_TEST = "KAFKA_TEST";

        static string KafkaTestString = "";

        public void ReceiveMessage(string _Message)
        {
            KafkaTestString = _Message;
        }

        [TestMethod]
        public void Test1_KafkaTest_Consumer()
        {
            KafkaTestString = "";
            KafkaConnectorSender = new cKafkaConnector("127.0.0.1:9092", "topic");
            KafkaConnectorSender.Consumer.StartListener("UnitTest", this);
        }


        [TestMethod]
        public void Test2_KafkaTest_ProduceAndControl()
        {
            KafkaConnectorReceiever = new cKafkaConnector("127.0.0.1:9092", "topic");
            KafkaConnectorReceiever.Producer.Init();
            KafkaConnectorReceiever.Producer.Produce("KAFKA_TEST");

            int __Counter = 0;
            while(__Counter < 30)
            {
                if (KafkaTestString == KAFKA_TEST)
                {
                    break;
                }
                Thread.Sleep(1000);
                __Counter++;
            }

            Assert.AreEqual(KafkaTestString, KAFKA_TEST);
        }

        [TestMethod]
        public void Test3_EventTest()
        {
            cEventGraph __EventGraph = new cEventGraph();
            WebApiController __ForTest = new WebApiController(__EventGraph);
            JObject __JsonObject = new JObject();
            JArray __JsonArray = new JArray();
            string __SampleJSON = "{\"app\":\"277cdc8c-b0ea-460b-a7d2-592126f5bbb0\",\"type\":\"HOTEL_CREATE\",\"time\":\"2020-02-10T13:40:27.650Z\",\"isSucceeded\":true,\"meta\":{},\"user\":{\"isAuthenticated\":true,\"provider\":\"b2c-internal\",\"email\":\"test1@hotmail.com\",\"id\":1},\"attributes\":{\"hotelId\":1,\"hotelRegion\":\"Kýbrýs\",\"hotelName\":\"Rixos\"}}";
            __JsonArray.Add(JObject.Parse(__SampleJSON));
            __JsonObject["events"] = __JsonArray;
            __ForTest.Events = __JsonObject;
            __EventGraph.Interpret(__ForTest);

            int __Counter = 0;
            while (__Counter < 30)
            {
                if (KafkaTestString != "" && KafkaTestString != KAFKA_TEST)
                {
                    break;
                }
                Thread.Sleep(1000);
                __Counter++;
            }

            cEventItem __EventItem = JsonConvert.DeserializeObject<cEventItem>(KafkaTestString);


            Assert.AreEqual(__EventItem.app.ToString(), "277cdc8c-b0ea-460b-a7d2-592126f5bbb0");
        }
    }
}