using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
namespace eventhubReceiverDirect
{
    public class PartitionReceiver : IPartitionReceiveHandler
    {
        public PartitionReceiver(string partitonID)
        {
            pID = partitonID;
            MaxBatchSize=10;
        }
        public string pID { get; set; }

        public int MaxBatchSize {get; set;}

        public Task ProcessErrorAsync(Exception error)
        {
            Console.WriteLine($"error {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(IEnumerable<EventData> events)
        {
            foreach (var eventData in events)
            {
                var dataJson = Encoding.UTF8.GetString(eventData.Body.Array);
                Console.WriteLine($"Reveived Event  Data{dataJson} | Partition {pID} | offset {eventData.SystemProperties.Offset}");
            }
        return Task.CompletedTask;

        }
    }
}