using ReferigenatorSvc.dbcontext;
using ReferigenatorSvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.Services
{
    public interface IReferigenatorService
    {
        public Task<bool> AddReferigenationItem(ItemsEntity itemEntity);
        public List<ItemViewModel> GetActiveReferigenationItems();

        public Task<List<ItemViewModel>> GetExpiredReferigenationItems();
        public Task<bool> UpsertReferigenratorItems(ItemsEntity items);
        public ItemlUpsertViewModel GetItemAndHistory(int itemId,bool IsIncludeHistory = true);
    }
}
