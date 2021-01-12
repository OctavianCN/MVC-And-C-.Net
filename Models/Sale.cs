using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace T_ShirtStore.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        [Required]
        [RegularExpression(@"^(?:100|[1-9]?[0-9])$",
       ErrorMessage = "Percentage is from 0 to 100.")]
        public double Percent { get; set; }

        //many to one
        public virtual ICollection<Kit> Kits { get; set; }
    }
}
