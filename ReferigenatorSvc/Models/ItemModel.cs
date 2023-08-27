using ReferigenatorSvc.Validator;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReferigenatorSvc.Models
{
    public class ItemViewModel 
    {
        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required]
        [Range(1, short.MaxValue)]
        [Display(Name = "Quantity")]
        public float ItemQuantity { get; set; }
    
        
        [Display(Name = "Consume Item")]
        [ComparaeValidator("ItemQuantity", typeof(float))]
        public float UpdateItemQuantity { get; set; }

        public int Id { get; set; }

        public string ItemType { get; set; }

        public DateTimeOffset UpdateDate { get; set; }

        public DateTimeOffset ExpirationDate { get; set; } = DateTime.UtcNow;
    }
}