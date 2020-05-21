using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs.Processor;
namespace eventHubIEventProcessorHost
{
    public class OffsetCheckpointManager : ICheckpointManager
    {
        public Task<bool> CheckpointStoreExistsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Checkpoint> CreateCheckpointIfNotExistsAsync(string partitionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateCheckpointStoreIfNotExistsAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteCheckpointAsync(string partitionId)
        {
            throw new NotImplementedException();
        }

        public Task<Checkpoint> GetCheckpointAsync(string partitionId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCheckpointAsync(Lease lease, Checkpoint checkpoint)
        {
            throw new NotImplementedException();
        }
    }
}