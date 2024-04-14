using EsearchSvc.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsearchSvc.Models.Entities
{
    public abstract class SearchBase
    {
        public virtual Task<bool> Search(string a){
            return Task.Run(() => { return true; });
        }

        public static  bool abc()
        {
            return true;
        }
        
    }

    public  class SearchBaseStatic
    {
        public static SearchBaseStatic searchBaseStatic;

        public SearchBaseStatic()
        {

        }
        static SearchBaseStatic()
        {
            searchBaseStatic = new SearchBaseStatic();
        }
    }

    

   

}
