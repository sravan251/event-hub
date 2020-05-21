using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs.Processor;
namespace eventHubIEventProcessorHost
{
    public class StorageLeaseManager : ILeaseManager
    {
        public TimeSpan LeaseRenewInterval => throw new NotImplementedException();

        public TimeSpan LeaseDuration => throw new NotImplementedException();

        public Task<bool> AcquireLeaseAsync(Lease lease)
        {
            throw new NotImplementedException();
        }

        public Task<Lease> CreateLeaseIfNotExistsAsync(string partitionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateLeaseStoreIfNotExistsAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteLeaseAsync(Lease lease)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLeaseStoreAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Lease>> GetAllLeasesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Lease> GetLeaseAsync(string partitionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LeaseStoreExistsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ReleaseLeaseAsync(Lease lease)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RenewLeaseAsync(Lease lease)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLeaseAsync(Lease lease)
        {
            throw new NotImplementedException();
        }
    }
}