using lab3_miercuri.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T_ShirtStore.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        [Required]
        [RegularExpression(@"^[\p{L}\p{N}]+$",
         ErrorMessage = "Only letters and numbers are allowed.")]
        public string TeamName { get; set; }
        // many-to-many relationship
        public virtual ICollection<Player> Players { get; set; }
        //many to one
        public virtual ICollection<Kit> Kits { get; set; }
        [NotMapped]
        public List<CheckBoxViewModel> PlayersList { get; set; }
    }
}