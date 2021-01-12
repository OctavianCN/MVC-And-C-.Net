using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace T_ShirtStore.Models
{
    public class Kit
    {
        public int KitId { get; set; }
        
        [Required]
        [RegularExpression(@"^[\p{L}\p{N}]+$",
         ErrorMessage = "Only letters and numbers are allowed.")]
        public string KitName { get; set; }
        // one to many
        public int SaleId { get; set;}
        public virtual Sale Sale { get; set; }
        //one to many
        public int SeasonId { get; set; }
        public virtual Season Season { get; set; }
        //one to many
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        //dropdown lists
        public IEnumerable<SelectListItem> SaleList { get; set; }
        public IEnumerable<SelectListItem> SeasonList { get; set; }
        public IEnumerable<SelectListItem> TeamList { get; set; }
    }
}