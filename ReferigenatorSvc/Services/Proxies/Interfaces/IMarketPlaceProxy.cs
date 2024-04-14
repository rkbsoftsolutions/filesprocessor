using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Svc.Services.Proxies.Interfaces
{
    interface IMarketPlaceProxy
    {
        public Task<object> GetItems();
    }
}
