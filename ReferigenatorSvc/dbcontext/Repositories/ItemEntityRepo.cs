using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReferigenatorSvc.dbcontext.Repositories
{
    public class ItemEntityRepo : Repository<ItemsEntity>, IItemEntityRepo
    {
        private AppDbContext _context;
        public ItemEntityRepo(DbContext context) : base(context)
        {
            _context = (AppDbContext)context;

        }

      
    }
}
