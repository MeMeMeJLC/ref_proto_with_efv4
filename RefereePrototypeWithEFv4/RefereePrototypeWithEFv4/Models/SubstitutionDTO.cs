using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefereePrototypeWithEFv4.Models
{
    public class SubstitutionDTO
    {
        public int Id { get; set; }
        public TimeSpan SubstitutionTime { get; set; }
        public int GamePlayerGoingOnId { get; set; }
        public string GamePlayerGoingOnFirstName { get; set; }
        public string GamePlayerGoingOnLastName { get; set; }
        public int GamePlayerGoingOffId { get; set; }
        public string GamePlayerGoingOffFirstName { get; set; }
        public string GamePlayerGoingOffLastName { get; set; }
        public string TeamName { get; set; }
        public int GameId { get; set; }
    }
}