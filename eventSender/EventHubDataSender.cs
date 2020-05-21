using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
namespace eventSender
{
    public interface IEventSender
    {
        Task SendDataAsync(byte[] data);

         Task SendDataAsync(IEnumerable<byte[]> datas);
    }
    public class EventHubDataSender: IEventSender
    {
         EventHubClient _eventHubClient;
        public EventHubDataSender(string eventHubConnectionString)
        {
             _eventHubClient=EventHubClient.CreateFromConnectionString(eventHubConnectionString);
        }
        public async Task SendDataAsync(byte[] data)
        {
            EventData dataToHub=new EventData(data);
            await  _eventHubClient.SendAsync(dataToHub);
        }

         public async Task SendDataAsync(IEnumerable<byte[]> datas)
        {
            var datasToHub=datas.Select(eventbytes=>new EventData(eventbytes));
            var eventHubBatch=_eventHubClient.CreateBatch();
            Parallel.ForEach(datasToHub,async (dataToHub)=>{
                if(!eventHubBatch.TryAdd(dataToHub))
                {
                    await _eventHubClient.SendAsync(eventHubBatch.ToEnumerable());
                    eventHubBatch=_eventHubClient.CreateBatch();
                    eventHubBatch.TryAdd(dataToHub);
                }
            });
            if(eventHubBatch.Count>0)
            await  _eventHubClient.SendAsync(datasToHub);
        }
    }
}
