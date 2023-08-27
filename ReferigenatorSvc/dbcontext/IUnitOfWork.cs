using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.dbcontext
{
  public  interface IUnitOfWork 
    {
        IItemEntityRepo ItemEntityRepo { get; }
        IItemEntityHistoryRepo ItemEntityHistoryRepo { get;}

       Task SaveChangesAsync();
        Task<IDbConnection> GetDbConnection();

    }
}
