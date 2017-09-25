using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dicer
{
    public class DiceFileDataStore : IDataStore<DiceSite>
    {
        public Task<bool> AddItemAsync(DiceSite item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DiceSite> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DiceSite>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(DiceSite item)
        {
            throw new NotImplementedException();
        }
    }
}
