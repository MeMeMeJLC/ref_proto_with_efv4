using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class GamePlayer
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public bool IsCaptain { get; set; }
        public bool IsStartingSubstitute { get; set; }

        //foreign key
        public int TeamId { get; set; }
        //navigation property
        public Team Team { get; set; }
    }
}