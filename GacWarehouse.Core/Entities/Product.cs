using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GacWarehouse.Core.Entities
{
    public class Product : BaseEntity
    {      
        [Required]
        public string Code { get; set; }        
        [Required]
        public string Name { get; set; }        
        [Required]
        public int Quantity { get; set; }        
        public decimal? PurchaseSingleItemPrice { get; set; }
        public string PurchaseSingleItemPriceCurrencyCode { get; set; }
        public int? ManufacturerId { get; set; }

        public double? DimensionLength { get; set; }
        public double? DimensionWidth { get; set; }
        public double? DimensionHeight { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

    }
}
