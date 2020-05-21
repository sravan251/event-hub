using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
namespace eventHubIEventProcessorHost
{
    public class SEventHubProcessor : IEventProcessor
    {
        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine($"shutting down the processor host {context.PartitionId} reason {reason}");
            return Task.CompletedTask;
        }

        public Task OpenAsync(PartitionContext context)
        {
             Console.WriteLine($"initilizing the  processor host for patition ID {context.PartitionId}");
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            Console.WriteLine($"Error  for partition {context.PartitionId} Error {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
             foreach (var eventData in messages)
            {
                var dataJson = Encoding.UTF8.GetString(eventData.Body.Array);
                Console.WriteLine($"Reveived Event  Data{dataJson} | Partition {context.PartitionId} | offset {eventData.SystemProperties.Offset}");
            }
        //this stores the current offset to azure blob storage
        return context.CheckpointAsync();
        }
    }
}