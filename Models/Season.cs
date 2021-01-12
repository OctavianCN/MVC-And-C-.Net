using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using T_ShirtStore.Models.MyValidations;

namespace T_ShirtStore.Models
{
    public class Season
    {
        public int SeasonId { get; set; }
        [Required]
        [SeasonNameValidator]
        public string SeasonName { get; set; }
        [Required]
        [RegularExpression(@"^[1-9]\d*$",
       ErrorMessage = "Price is a positive number")]
        public double Price { get; set; }

        //many to one
        public virtual ICollection<Kit> Kits { get; set; }
    }
    
}