using EsearchSvc.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsearchSvc.Models.Entities
{

    public class SearchCustomerRef : ISearchRefer<SearchCustomerRef>
    {
        public Task Consume()
        {
            return Task.Run(() => { return true; });
        }
    }
}
