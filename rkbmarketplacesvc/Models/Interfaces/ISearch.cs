using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsearchSvc.Models.Interfaces
{
   public interface ISearch
    {

        Task<bool> Search();

        public bool Search1();
         public int MyProperty { get; set; }
        
    }

    public interface ISearchRefer<T> where T : class, ISearchRefer<T>
    {
        Task Consume();
    }
}
