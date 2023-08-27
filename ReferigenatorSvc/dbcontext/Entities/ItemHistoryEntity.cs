using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.dbcontext
{

    public class ItemHistoryEntity : EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public float ItemQuantity { get; set; }

        public float UsedQuantity { get; set; }

        public int ItemId { get; set; }

        public ItemsEntity ItemsEntity { get; set; }

    }
}
