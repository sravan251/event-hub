using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs.Processor;

namespace eventHubIEventProcessorHost
{
    class Program
    {
        private static readonly string eventHubPath="azuredeploytemplates";
        private static readonly string eventHubConnectionString=ConfigurationManager.AppSettings["eventHubconnectionString"].ToString();

        private static readonly string consumerGroupName="";
        private static readonly string storageConnectionString="";
        private static readonly string leaseContainerName="";



        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static async Task MainAsync()
        {
            Console.WriteLine($"register the processor Host{nameof(SEventHubProcessor)}");
            EventProcessorHost eventProcessorHost=new EventProcessorHost(eventHubPath
            ,consumerGroupName,eventHubConnectionString,storageConnectionString,leaseContainerName);
            await eventProcessorHost.RegisterEventProcessorAsync<SEventHubProcessor>();
             Console.WriteLine("Waiting for incomming Events..");
            Console.WriteLine("press any key to shutdown");
           Console.ReadKey();
           await eventProcessorHost.UnregisterEventProcessorAsync();
        }

    }
}
