
using Core.nDTOs.nEvent;
using Core.nDTOs.nEvent.nEventItem;
using Core.nKafkaConnector;
using Core.nUtils.nJsonConverter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace MicroServiceSample.nWebGraph
{ 
    public class cEventGraph 
    {
        protected cKafkaConnector KafkaConnector { get; set; }
        public cEventGraph()
            :base()
        {
            KafkaConnector = new cKafkaConnector("127.0.0.1:9092", "topic");
            //KafkaConnector = new cKafkaConnector("host.docker.internal:29092", "topic");
            KafkaConnector.Producer.Init();
        }

        public void Interpret(cBaseController _Controller)
        {
            try
            {

                cEvents __Event = null;
                if (_Controller.Events.ContainsKey("events"))
                {
                    JToken __Events = _Controller.Events["events"];

                    JsonSerializerSettings __Settings = new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter> { new cBadDateFixingConverter() },
                        DateParseHandling = DateParseHandling.None
                    };


                    __Event = JsonConvert.DeserializeObject<cEvents>(_Controller.Events.ToString(), __Settings);

                    foreach (cEventItem __Item in __Event.events)
                    {
                        ///Validatora gönderilecek
                        KafkaConnector.Producer.Produce(JsonConvert.SerializeObject(__Item));
                    }

                    Console.WriteLine(JsonConvert.SerializeObject(__Event));
                }
            }
            catch (Exception _Ex)
            {
               //Exception Handling
            }
            
        }

    }
}
