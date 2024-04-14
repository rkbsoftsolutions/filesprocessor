using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.Models
{
    public class ItemlUpsertViewModel
    {
        public ItemViewModel ItemViewModel { get; set; }

        public List<StorageTypes> storageTypes { get; set; }
        public List<ItemViewModel> currentActiveItems { get; set; }
    }
}
