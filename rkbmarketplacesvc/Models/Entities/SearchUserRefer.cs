using EsearchSvc.Models.Interfaces;
using rkbmarketplace.ExternalProxies.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EsearchSvc.Models.Entities
{
    public class SearchUserRefer : ISearchRefer<SearchUserRefer>
    {
        public Task Consume()
        {
            return Task.Run(() => { return true; });

        }
    }

    public interface ISearchServiceResolver
    {
        public ISearch Resolve(string key);
    }

    public class SearchServiceResolver : ISearchServiceResolver
    {
        public readonly IServiceProvider _provider;
        public SearchServiceResolver(IServiceProvider provider)
        {
            _provider = provider;
        }
        public ISearch Resolve(string name)
        {
            var type = Assembly.GetAssembly(typeof(SearchServiceResolver)).GetType($"{name}");
            // var instance = this._provider.GetService(type);
            return default(ISearch);
            //return new FlipKart() 
        }
    }



}
