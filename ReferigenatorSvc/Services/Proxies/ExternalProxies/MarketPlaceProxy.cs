using Svc.Services.Proxies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Svc.Services.Proxies.ExternalProxies
{
    public class MarketPlaceProxy : IMarketPlaceProxy
    {
        public Task<object> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
