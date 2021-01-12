using lab3_miercuri.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace T_ShirtStore.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        [RegularExpression(@"^[\p{L}]+$",
        ErrorMessage = "Only letters are allowed.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[\p{L}]+$",
         ErrorMessage = "Only letters are allowed.")]
        public string LastName { get; set; }
        // many-to-many relationship
        public virtual ICollection<Team> Teams { get; set; }

        [NotMapped]
        public List<CheckBoxViewModel> TeamsList { get; set; }
    }
}