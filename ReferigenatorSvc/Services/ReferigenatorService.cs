using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReferigenatorSvc.dbcontext;
using ReferigenatorSvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Text;

namespace ReferigenatorSvc.Services
{
    public class ReferigenatorService : IReferigenatorService
    {
        private readonly List<StorageTypes> _storageTyeps;
        public readonly IUnitOfWork _unitOfWork;
        public ReferigenatorService(IUnitOfWork unitOfWork, IOptions<List<StorageTypes>> storageTyps)
        {
            _unitOfWork = unitOfWork;
            _storageTyeps = storageTyps.Value;
        }
        public async Task<bool> AddReferigenationItem(ItemsEntity itemEntity)
        {
            await _unitOfWork.ItemEntityRepo.Add(itemEntity).ConfigureAwait(false);
            var history = itemEntity.Adapt<ItemHistoryEntity>();
            history.ItemId = itemEntity.Id;
            itemEntity.ItemHistoryEntities = new List<ItemHistoryEntity> { history };
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

       
        public List<ItemViewModel> GetActiveReferigenationItems()
        {
           
            var currentItems = _unitOfWork.ItemEntityRepo.Find(x => x.IsActive == true && x.ItemQuantity > 0).ToList();
            currentItems.ForEach(x => x.ItemType = _storageTyeps.Single(s => s.Code == x.ItemType).Name);
           return currentItems.Adapt<List<ItemViewModel>>();
        }

        public ItemlUpsertViewModel GetItemAndHistory(int itemId, bool IsIncludeHistory = true)
        {

           var Item = _unitOfWork.ItemEntityRepo.Find(x => x.IsActive == true && x.Id== itemId).First();
            var ItemUpserViewModel = new ItemlUpsertViewModel();
            if (Item != null)
            {
                ItemUpserViewModel.ItemViewModel = Item.Adapt<ItemViewModel>();
                if (IsIncludeHistory)
                {
                    var history = _unitOfWork.ItemEntityHistoryRepo.Find(x => x.ItemId == Item.Id)
                        .OrderByDescending(x => x.CreateDate);
                    if (history != null)
                        ItemUpserViewModel.currentActiveItems = history.Adapt<List<ItemViewModel>>();
                }
            }
            return ItemUpserViewModel;
        }

        public async Task<bool> UpsertReferigenratorItems(ItemsEntity items)
        {
            var currentItem = await _unitOfWork.ItemEntityRepo.GetFirstOrDefault(x => x.Id == items.Id).ConfigureAwait(false);
            var history = items.Adapt<ItemHistoryEntity>();
            history.ItemId = items.Id;
            history.Id = 0;
            history.UsedQuantity = currentItem.ItemQuantity - items.ItemQuantity; // 10 - 8 
            currentItem.ItemQuantity = items.ItemQuantity;
            history.UpdateDate = DateTime.Now;
            currentItem.ItemHistoryEntities = new List<ItemHistoryEntity> { history };
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<ItemViewModel>> GetExpiredReferigenationItems()
        {
           /* SQLLIte Entity framwork not supported Date so used Ado.net core */

            StringBuilder db = new StringBuilder();
            db.AppendLine("SELECT Id, ItemName, ItemQuantity, ItemType, ExpirationDate, CreateDate, ");
            db.AppendLine(" UpdateDate,IsActive FROM itemsEntities");
            db.AppendLine(" where date(ExpirationDate, 'utc') < date('now', 'utc')");
            using (var connection = await _unitOfWork.GetDbConnection().ConfigureAwait(false))
            {
                using IDbCommand dbCommand = connection.CreateCommand();
                dbCommand.CommandText = db.ToString();
                dbCommand.CommandType = CommandType.Text;
                IDataReader dr = dbCommand.ExecuteReader(CommandBehavior.Default);
                List<ItemViewModel> itemViewModels = new List<ItemViewModel>();
                while (dr.Read())
                {
                    itemViewModels.Add(new ItemViewModel
                    {
                        ExpirationDate = Convert.ToDateTime(dr["ExpirationDate"]),
                        ItemType = _storageTyeps.Single(s => s.Code == dr["ItemType"].ToString()).Name,
                        ItemName = dr["ItemType"].ToString()
                    });
                }
                return itemViewModels;
            }

            
        }
    }
}