using ReferigenatorSvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Svc.Services
{
   public interface IStoreService
    {
       Task<List<ItemViewModel>> GetActiveRefrigenationItemsByUserId(Guid userId);

        Task<ItemlUpsertViewModel> GetItemAndHistory(int Id);

        Task<ItemlUpsertViewModel> GetItemAndHistory(int id, bool included);

        Task<bool> UpsertRefrigenratorItems(ItemViewModel itemViewModel);

        Task<bool> AddItem(Guid UserId, ItemViewModel itemViewModel);

        Task<List<ItemViewModel>> GetExpiredRefrigenationItems();
    }
}
