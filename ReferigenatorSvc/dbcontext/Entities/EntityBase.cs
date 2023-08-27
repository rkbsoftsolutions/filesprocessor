using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.dbcontext
{
    public abstract class EntityBase
    {
        public DateTimeOffset CreateDate { get; set; }

        public DateTimeOffset UpdateDate { get; set; }

        public bool IsActive { get; set; }
    }
}
