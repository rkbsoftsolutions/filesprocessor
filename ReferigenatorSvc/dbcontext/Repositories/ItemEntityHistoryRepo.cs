using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.dbcontext.Repositories
{
    public class ItemEntityHistoryRepo : Repository<ItemHistoryEntity>, IItemEntityHistoryRepo
    {
        private AppDbContext _context;
        public ItemEntityHistoryRepo(DbContext context) : base(context)
        {
            _context = (AppDbContext)context;

        }
    }
}
