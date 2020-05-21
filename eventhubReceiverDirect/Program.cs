using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Text;
using Microsoft.Azure.EventHubs;

namespace eventhubReceiverDirect
{
    class Program
    {
        //string eventHubconnectionString=ConfigurationManager.AppSettings["eventHubconnectionString"].ToString();
        static readonly string eventHubConnectionString = ConfigurationManager.AppSettings["eventHubconnectionString"].ToString();

        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        public static async Task MainAsync()
        {
            Console.WriteLine("Connecting to Event Hub..");
            var eventHubClient = EventHubClient.CreateFromConnectionString(eventHubConnectionString);
            
            var runtimeInformation=await eventHubClient.GetRuntimeInformationAsync();
            var partitionReceiviers=runtimeInformation.PartitionIds.Select(partitionId=>
                                    eventHubClient.CreateReceiver("$Default", partitionId, EventPosition.FromStart())
            ).ToList();
            Console.WriteLine("Waiting for incomming event..");
           foreach(Microsoft.Azure.EventHubs.PartitionReceiver receiver in partitionReceiviers)
           {
               receiver.SetReceiveHandler(new PartitionReceiver(receiver.PartitionId));
           }
           Console.WriteLine("press any key to shutdown");
           Console.ReadKey();
           await eventHubClient.CloseAsync();
            // while (true)
            // {
            //     var eventDatas = await partitionReceiver.ReceiveAsync(10);
            //     if (eventDatas != null)
            //     {
            //         foreach (var eventData in eventDatas)
            //         {
            //             var dataJson = Encoding.UTF8.GetString(eventData.Body.Array);
            //             Console.WriteLine($"Reveived Event  Data{dataJson} | Partition {partitionReceiver.PartitionId}");
            //         }
            //     }
            // }
        }

    }
}
