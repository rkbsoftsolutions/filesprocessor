using ReferigenatorSvc.dbcontext;
using ReferigenatorSvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.Services
{
    public interface IRefrigenatorService
    {
        public Task<bool> AddRefrigenationItem(ItemsEntity itemEntity);
        public List<ItemViewModel> GetActiveRefrigenationItems();

        public Task<List<ItemViewModel>> GetExpiredRefrigenationItems();
        public Task<bool> UpsertRefrigenratorItems(ItemsEntity items);
        public ItemlUpsertViewModel GetItemAndHistory(int itemId,bool IsIncludeHistory = true);
    }
}
