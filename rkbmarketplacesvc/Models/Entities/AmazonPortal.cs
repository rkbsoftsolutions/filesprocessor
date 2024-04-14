using EsearchSvc.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsearchSvc.Models.Entities
{
    public class AmazonPortal : ISearch
    {
        public int MyProperty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task<bool> Search()
        {
            throw new NotImplementedException();
        }

        public bool Search1()
        {
            throw new NotImplementedException();
        }
    }
}
