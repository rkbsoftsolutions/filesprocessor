using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AuthenticationSvc.Entities
{
    public abstract class Tenent
    {
        public abstract string EntityName { get; }

    }

    public interface ITenant
    {
        public DateTime LastUpdate { get; set; }

        public DateTime CreateDdate { get; set; }
    }
   public class NewsEntity : Tenent , ITenant
    {
        public NewsEntity()
        {
            this.AllowComments = this.Published = this.LimitedToStores = true;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }

        public string ShortDetail { get; set; }

        public string FullDetail { get; set; }

        public override string EntityName =>  nameof(NewsEntity);

        public DateTimeOffset CreatedOnUtc { get; set; }

        public int LanguageId { get; set; }

        public bool Published { get; set; } 
        public bool AllowComments { get; set; }
        public bool LimitedToStores { get; set; }
        public string EntityName1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime LastUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CreateBy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime CreateDdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
