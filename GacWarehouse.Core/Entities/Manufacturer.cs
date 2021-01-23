using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GacWarehouse.Core.Entities
{
    public class Manufacturer : BaseEntity
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Rating { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
