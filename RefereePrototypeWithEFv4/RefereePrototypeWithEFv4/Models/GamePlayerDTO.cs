using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class GamePlayerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsCaptain { get; set; }
        public bool IsStartingSubstitute { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int GameId { get; set; }
    }
}