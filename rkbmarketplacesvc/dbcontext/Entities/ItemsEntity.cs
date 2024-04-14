using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReferigenatorSvc.dbcontext
{
    public class ItemsEntity: EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ItemName { get; set; }

        public float ItemQuantity { get; set; }

        public string ItemType { get; set; }

        public Guid userId { get; set; }

        public DateTimeOffset ExpirationDate { get; set; }
        public ICollection<ItemHistoryEntity> ItemHistoryEntities { get; set; }

    }
}
